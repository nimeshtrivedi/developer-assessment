﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models.ViewModels
{
    public class TodoItemAddVm
    {
        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
