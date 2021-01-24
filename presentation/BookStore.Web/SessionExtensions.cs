﻿using BookStore.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace BookStore.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";

        /*public static int? GetInt32(string cellPhone)
        {
            return null;
        }
        public static void SetInt32(string cellPhone, int code)
        {
        }*/
        public static void Set(this ISession session, Cart value)
        {
            if(value  == null)
            {
                return;
            }

            using(var stream = new MemoryStream())
            using(var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);
                               
                session.Set(key, stream.ToArray());     
            }
          
        }

        public static bool TryGetCart(this ISession session, out Cart value)
        {
            if(session.TryGetValue(key, out byte[] buffer))
            {
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var orderId = reader.ReadInt32();
                    var totalCount = reader.ReadInt32();
                    var totalPrice = reader.ReadDecimal();

                    value = new Cart(orderId)
                    {
                        TotalCount = totalCount,
                        TotalPrice = totalPrice
                    };

                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}
