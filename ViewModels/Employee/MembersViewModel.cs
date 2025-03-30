using Library.DAO.MySQL;
using Library.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models.Entities;

namespace Library.ViewModels.Employee
{
    class MembersViewModel : BaseViewModel
    {
        private readonly IMember _memberDAO;

        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> Members
        {
            get => _members;
            set
            {
                if (SetProperty(ref _members, value))
                {
                    OnPropertyChanged(nameof(MembersCount));
                    _members.CollectionChanged += (s, e) => OnPropertyChanged(nameof(MembersCount));
                }
            }
        }

        public int MembersCount => Members?.Count ?? 0;

        public MembersViewModel()
        {
            _memberDAO = new MemberDAO();
            LoadMembers();
        }

        private async void LoadMembers()
        {
            Members = new ObservableCollection<Member>(await _memberDAO.GetAllMembersAsync());
        }
    }
}
