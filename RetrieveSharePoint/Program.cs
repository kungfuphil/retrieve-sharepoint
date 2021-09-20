using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;

namespace RetrieveSharePoint
{
    class Program
    {
        static void Main()
        {
            ClientContext context = new ClientContext("https://thewebsite.com/sites/TheSharePointSite");

            List list = context.Web.Lists.GetByTitle("The List I Want");

            CamlQuery query = new CamlQuery
            {
                ViewXml = @"<View>
                    <Query>
                        <Where>
                            <FieldRef Name='ID'/>
                            <Value Type='Integer'>1</Value>
                        </Where>
                    </Query>
                    <RowLimit>10</RowLimit>
                </View>"
            };

            ListItemCollection items = list.GetItems(query);

            context.Load(items);
            context.ExecuteQuery();

            foreach (ListItem item in items)
            {
                foreach (KeyValuePair<string, object> kvp in item.FieldValues)
                {
                    Console.WriteLine($"[{kvp.Key}]: {kvp.Value}");
                }

                Console.WriteLine($"ID: {item["ID"]}");
                Console.WriteLine($"Title: {item["Title"]}");
                Console.WriteLine($"Description: {item["field2"]}");
                Console.WriteLine($"Created By: {((FieldUserValue)item["Author"]).LookupValue}");
                Console.WriteLine();
            }
        }
    }
}
