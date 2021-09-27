﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages.Events.Abstract
{
    public interface IStockQuantityReducedEvent
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
