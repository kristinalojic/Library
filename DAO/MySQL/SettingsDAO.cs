using Library.Models;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.MySQL
{
    class SettingsDAO : ISettings
    {
        public async Task<Setting> GetSettingByIdAsync(int id)
        {
            using (var _db = new LibraryDbContext())
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(s => s.Employee == id);
                return setting;
            }
        }

        public async void SaveSettingsByIdAsync(int id, string theme, string color, string language)
        {
            using (var _db = new LibraryDbContext())
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(s => s.Employee == id);
                if (setting != null)
                {
                    setting.Theme = theme;
                    setting.Color = color;
                    setting.Language = language;
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
