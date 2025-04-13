using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using api.Dtos.PatientData;
using api.Models;
using Newtonsoft.Json;

namespace api.Mappers
{
    public static class PatientDataMappers
    {
        public static JSONDataDto ToJSON(this List<PatientData> dataList)
        {

            JSONDataDto returnItem = new JSONDataDto();

            foreach (PatientData item in dataList) {

                returnItem[item.Property] = item.Value;
            }

            return returnItem;
        }

        public static JSONDataDto ToJSON(this PatientData dataItem) {
            
            return new JSONDataDto() {
                    {dataItem.Property, dataItem.Value},
            };
        }
    }
}