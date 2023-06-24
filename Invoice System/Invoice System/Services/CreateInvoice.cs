using Invoice_System.Models;
using System;


namespace Invoice_System.Services
{
    public class CreateInvoice
    {
        public bool remaining =false;
        public Invoice GenerateInvoice(List<RequestLineItems> request_line_items, ProductsDetails products_data)
        {
            RandomNumberGenerater randomNumber = new RandomNumberGenerater();

            string msg = "";

            Invoice invoice = new Invoice
            {
                InvoiceIssueDate = DateTime.Now,
                InvoiceNumber = "INV " + randomNumber.Rand(),
                LineItems = new List<LineItems>()
            };
            
            dynamic products = products_data.Products;

            foreach (var lineItem in request_line_items)
            {
                var productCode = lineItem.Code;
                var quantity = lineItem.Qty;

                if (products.Count > 0) {
                    foreach (var product in products)
                    {
                        if (product.Code.Equals(productCode))
                        {
                            
                            LineItems lineItems = new LineItems
                            {
                                Description = product.Name,
                                Code = productCode,
                                TotalItemQty = quantity,
                                Packs = new List<InvoicePacks>()
                            };
                            var remainingQty = quantity;

                            List<Packs> packs = product.Packs;

                            List<Packs> sortedList = packs.OrderByDescending(x => x.Qty).ToList();

                            for(int i = 0; i < sortedList.Count; i++)
                            {
                                FindCombinationsHelper(remainingQty, remainingQty, lineItems, sortedList, i);
                                break;
                            }

                            lineItems.LineItemTotal = lineItems.Packs.Sum(p => p.Price * p.Qty);
                            invoice.LineItems.Add(lineItems);
                        }
                    }
                }
            }
            invoice.InvoiceTotal = invoice.LineItems.Sum(li => li.LineItemTotal);
            return invoice;
            
        }

        public void FindCombinationsHelper(int remainingQty, int fixedQty, LineItems lineItems, List<Packs> sortedList,int i )
        {
            List<Packs> new_packs = new List<Packs>();
            if ((remainingQty % sortedList[i].Qty) == 0)
            {
                var packQty = remainingQty / sortedList[i].Qty;

                lineItems.Packs.Add(new InvoicePacks
                {
                    PackageCode = sortedList[i].Name,
                    ItemsPerPack = sortedList[i].Qty,
                    Price = sortedList[i].Price,
                    Qty = packQty
                });
            }
            else
            {
                if (remainingQty >= sortedList[i].Qty)
                {
                    var packQty = remainingQty / sortedList[i].Qty;
                    remainingQty %= sortedList[i].Qty;
                    lineItems.Packs.Add(new InvoicePacks
                    {
                        PackageCode = sortedList[i].Name,
                        ItemsPerPack = sortedList[i].Qty,
                        Price = sortedList[i].Price,
                        Qty = packQty
                    });
                    new_packs.AddRange(sortedList.Skip(i++));
                    for (int j = 0; j < new_packs.Count; j++)
                    {
                        if ((remainingQty % new_packs[j].Qty) == 0)
                        {
                            remaining = false;
                            packQty = remainingQty / new_packs[j].Qty;
                            lineItems.Packs.Add(new InvoicePacks
                            {
                                PackageCode = new_packs[j].Name,
                                ItemsPerPack = new_packs[j].Qty,
                                Price = new_packs[j].Price,
                                Qty = packQty
                            });
                            break;
                        }
                        else
                        {
                            remaining = true;
                        }

                    }
                }
                if (remaining)
                {
                    lineItems.Packs.Clear();
                    new_packs.Clear();
                    remainingQty = fixedQty;
                    if (sortedList.Count > i)
                    {
                        FindCombinationsHelper(remainingQty, remainingQty, lineItems, sortedList, i++);
                    }
                    else
                    {
                        // No Packs
                        Console.WriteLine("No Packs");
                    }
                }
            }
        }
    }
}
