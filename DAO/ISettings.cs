using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface ISettings
    {
        Task<Setting> GetSettingByIdAsync(int id);

        void SaveSettingsByIdAsync(int id, String theme, String color, String language);
    }
}
