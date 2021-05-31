using System.Collections.Generic;
using System.Linq;

namespace BankAccountApi.Helpers
{
    public class AddressHelper
    {
        public static IDictionary<string, IList<string[]>> StatePostCodeMap { get; set; } = new Dictionary<string, IList<string[]>>() {
            {"NSW", new List<string[]>{
                new string[]{"1000", "1999"},
                new string[]{"2000", "2599"},
                new string[]{"2619", "2899"},
                new string[]{"2921", "2999"}}},
            {"ACT", new List<string[]>{
                new string[]{"0200", "0299"},
                new string[]{"2600", "2618"},
                new string[]{"2900", "2920"}}},
            {"VIC", new List<string[]>{
                new string[]{"3000", "3999"},
                new string[]{"8000", "8999"}}},
            {"QLD", new List<string[]>{
                new string[]{"4000", "4999"},
                new string[]{"9000", "9999"}}},
            {"SA", new List<string[]>{
                new string[]{"5000", "5799"},
                new string[]{"5800", "5999"}}},
            {"WA", new List<string[]>{
                new string[]{"6000", "6797"},
                new string[]{"6800", "6999"}}},
            {"TAS", new List<string[]>{
                new string[]{"7000", "7799"},
                new string[]{"7800", "7999"}}},
            {"NT", new List<string[]>{
                new string[]{"0800", "0899"},
                new string[]{"0900", "0999"}}},


        };
        public static bool ValidStateWithPostCode(string state, string postCode)
        {
            StatePostCodeMap.TryGetValue(state, out IList<string[]> postCodeList);
            for (int i = 0; i < postCodeList.Count; i++) {
                var from = postCodeList[i][0];
                var to = postCodeList[i][1];
                if (postCode.CompareTo(from) >= 0 && postCode.CompareTo(to) <= 0) {
                    return true;
                }
            }
            return false;
        }
    }
}