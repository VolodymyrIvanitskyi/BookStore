using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Tests
{
    public class OrderItemCollectionTests
    {
        [Fact]
        public void Get_WithExcitingItem_ReturnsItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            var orderItem = order.Items.Get(2);

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

            Assert.Throws<InvalidOperationException>(() =>
            {
                var orderItem = order.Items.Get(100);
            });
        }

        [Fact]
        public void Add_WithExcitingItem_ThrowInvalidOperationException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Add(1, 10, 10m);
            }
            );
        }

        [Fact]
        public void Add_WithNonExcitingItem_SetsCount()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            order.Items.Add(3, 10, 0m);

            Assert.Equal(10 + 0, order.Items.Get(3).Count);
        }

        [Fact]
        public void Remove_WithExcitingItem_RemovesItem()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,2,10m),
                new OrderItem(2,5,20m)
            });

            order.Items.Remove(2);

            Assert.Collection(order.Items,
                item => Assert.Equal(1, item.BookId));
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
                order.Items.Remove(100);
            });
        }
    }
}
