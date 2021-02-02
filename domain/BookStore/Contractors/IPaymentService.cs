﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Contractors
{
    public interface IPaymentService
    {
        string Name { get; }
        string Title { get; }

        Form FirstForm(Order order);

        Form NextForm(int step, IReadOnlyDictionary<string, string> value);

        OrderPayment GetPayment(Form form);

    }
}
