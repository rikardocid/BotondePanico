using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Log
{
    public interface ICustomSystemLog
    {
        bool AddLog(SystemLog log);
    }
}
