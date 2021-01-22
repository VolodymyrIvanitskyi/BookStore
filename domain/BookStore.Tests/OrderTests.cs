using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_WithNullItems_ThrowsArgumentNullexception()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(1, null));
        }

        [Fact]
        public void TotalCount_WithEmptyItems_ReturnsZero()
        {
            var order = new Order(1, new OrderItem[0]);
            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnsZero()
        {
            var order = new Order(1, new OrderItem[0]);
            Assert.Equal(0m, order.TotalPrice);
        }

        [Fact]
        public void TotalCount_WithNonEmptyItems_CalculatesTotalCount()
        {
            var order = new Order(1, new []
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });


            Assert.Equal(2+5, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyItems_CalculatesTotalPrice()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });


            Assert.Equal(2*10m+5*20m, order.TotalPrice);
        }

        [Fact]
        public void Get_WithExcitingItem_ReturnsItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            var orderItem = order.GetItem(2);

            Assert.Equal(5, orderItem.Count);
        }

        [Fact]
        public void Get_WithNonExcitingItem_throwsInvalidOperationException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            Assert.Throws<InvalidOperationException>(()=>
            { 
                var orderItem = order.GetItem(100); 
            });
        }
        
        [Fact]
        public void AddOrUpdateItem_WithExcitingItem_UpdatesCount()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            var book = new Book(1, null, null, null, null, 0m);

            order.AddorUpdateItem(book, 10);
            Assert.Equal(10 + 2, order.GetItem(1).Count);
        }

        [Fact]
        public void AddOrUpdateItem_WithNonExcitingItem_AddsCount()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            var book = new Book(3, null, null, null, null, 0m);

            order.AddorUpdateItem(book, 10);
            Assert.Equal(10 + 0, order.GetItem(3).Count);
        }

        [Fact]
        public void Remove_WithExcitingItem_RemovesItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            order.RemoveItem(2);

            Assert.Equal(1, order.Items.Count); //Після видалення кількість OrderItem == 1(бо було 2) 
        }

        [Fact]
        public void Remove_WithNonExcitingItem_throwsInvalidOperationException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.RemoveItem(100);
            });
        }

    }
}
