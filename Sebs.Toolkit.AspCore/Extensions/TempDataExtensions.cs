using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Sebs.Toolkit.AspCore.Extensions
{
    /// <summary>
    /// Purpose: You can store simple values in TempData - strings, booleans and numeric types, 
    /// but if you try to store complex types, you will encounter an InvalidOperationException:
    /// *The '[name of the property]' property with TempDataAttribute is invalid.
    /// *The 'Microsoft.AspNetCore.Mvc.ViewFeatures.Internal.TempDataSerializer' cannot serialize an object of type '[name of the property]'.
    /// 
    /// If you want to use TempData to store complex types, you must serialize it to a string-based format yourself.
    /// JSON is the recommended format to use because it is relatively compact(compared to XML) and JSON.Net is included as part of the default project template.
    /// The following class contains two extension methods: one for serialising data to JSON and the other for deserialising it:
    /// Created by: sebde
    /// Created at: 1/3/2021 12:09:03 PM
    /// </summary>
    /// <remarks>
    /// https://www.learnrazorpages.com/razor-pages/tempdata
    /// </remarks>
    /// <usage>
    /// TempData.Insert("myData", new SomeTestViewModel());
    /// ...
    /// var data = TempData.Extract<SomeTestViewModel>("myData");
    /// </usage>
    public static class TempDataExtensions
    {
        public static void Insert<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T? Extract<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            _ = tempData.TryGetValue(key, out object? o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
