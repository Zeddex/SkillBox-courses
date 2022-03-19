using System;
using System.Collections.Generic;
using Homework_20.Models;

namespace Homework_20.MemoryStorage
{
    public interface INoteStorage
    {
        Note GetById(int id);
        List<Note> GetAll();
    }
}
