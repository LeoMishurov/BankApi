using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankClient
{
    public static class AsyncDataProvider

    {    

    private const int _DefaultDelayTime = 300;



    public static ReadOnlyCollection<string> GetItems()

    {

        return GetItems(_DefaultDelayTime);

    }


    public static ReadOnlyCollection<string> GetItems(int delayTime)

    {           

        List<string> items = new List<string>();        

        foreach (string item in Enum.GetNames(typeof(AttributeTargets)).OrderBy(item => item.ToLower()))

        {

            items.Add(item);

            //// Syntetic delay to emulate a long items getting process<br/>

            //Thread.Sleep(delayTime);

        }



        return items.AsReadOnly();

    }        

}
}
