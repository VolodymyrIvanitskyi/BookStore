using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void OrderItem_WithZeroCount_throwArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = 0;
                new OrderItem(1, count, 12m);
            });
        }

        [Fact]
        public void OrderItem_WithNegativeCount_throwArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = -2;
                new OrderItem(1, count, 12m);
            });
        }

        [Fact]
        public void OrderItem_WithPositiveCount_SetsCount()
        {
            var orderItem = new OrderItem(1, 2, 3m);
            Assert.Equal(1, orderItem.BookId);
            Assert.Equal(2, orderItem.Count);
            Assert.Equal(3m, orderItem.Price);

        }

        [Fact]
        public void Count_WithNegativeValue_throwsArgumentOutOfRangeException()
        {
            var orderItem = new OrderItem(1, 5, 12m);
            
            Assert.Throws<ArgumentOutOfRangeException>(()=>
            {
                orderItem.Count = -1;
            });
        }

        [Fact]
        public void Count_WithZeroValue_throwsArgumentOutOfRangeException()
        {
            var orderItem = new OrderItem(1, 5, 12m);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                orderItem.Count = 0;
            });
        }

        [Fact]
        public void Count_WithPositiveValue_setsValue()
        {
            var orderItem = new OrderItem(1, 5, 12m);
            orderItem.Count = 2;

            Assert.Equal(2, orderItem.Count);
        }
    }
}
