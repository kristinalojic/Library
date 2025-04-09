using Library.DAO.MySQL;
using Library.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models.Entities;
using System.Windows.Input;
using Employee_And_Company_Management.Commands;
using Library.ViewModels.Admin;
using Library.Views.Windows.Admin;
using Library.Views.Windows;
using Library.Views.Windows.Employee;
using System.Text.RegularExpressions;

namespace Library.ViewModels.Employee
{
    public class MembersViewModel : BaseViewModel
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

        private ObservableCollection<Member> _filteredMembers;
        public ObservableCollection<Member> FilteredMembers
        {
            get => _filteredMembers;
            set
            {
                if (SetProperty(ref _filteredMembers, value))
                {
                    _filteredMembers.CollectionChanged += (s, e) => OnPropertyChanged(nameof(MembersCount));
                }
            }
        }

        public int MembersCount => FilteredMembers?.Count ?? 0;

        private Member _selectedMember;
        public Member SelectedMember
        {
            get => _selectedMember;
            set => SetProperty(ref _selectedMember, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    SetProperty(ref _searchText, value);
                    FilterMembers();
                }
            }
        }

        public ICommand AddMemberCommand { get; set; }
        public ICommand ViewDetailsCommand { get; set; }

        private MembersViewModel()
        {
            _memberDAO = new MemberDAO();

            AddMemberCommand = new RelayCommand(AddMember, CanAddMember);
            ViewDetailsCommand = new RelayCommand(ViewDetails, CanViewDetails);
        }

        public static async Task<MembersViewModel> CreateAsync()
        {
            var viewModel = new MembersViewModel();
            await viewModel.LoadMembers();
            return viewModel;
        }

        public async Task LoadMembers()
        {
            Members = new ObservableCollection<Member>(await _memberDAO.GetAllMembersAsync());
            FilterMembers();
        }

        private void AddMember(object? obj)
        {
            AddMemberWindow window = new AddMemberWindow(this);
            window.Show();
        }

        private bool CanAddMember(object? obj) => true;

        private void ViewDetails(object? obj)
        {
            var _pomMember = SelectedMember;
            if (_pomMember != null)
            {
                MemberDetailsWindow window = new MemberDetailsWindow(this, _pomMember);
                window.Show();
            }
            else
            {
                var messageBox = new CustomMessageBox("Selektujte clana.");
                messageBox.ShowDialog();
            }
        }

        private bool CanViewDetails(object? obj) => true;

        private void FilterMembers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredMembers = new ObservableCollection<Member>(_members);
            }
            else
            {
                var searchParts = SearchText?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (searchParts?.Length == 2)
                {
                    string firstName = searchParts[0];
                    string lastName = searchParts[1];

                    FilteredMembers = new ObservableCollection<Member>(
                        _members.Where(m =>
                            m.Name.StartsWith(firstName, StringComparison.OrdinalIgnoreCase) &&
                            m.Surname.StartsWith(lastName, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    FilteredMembers = new ObservableCollection<Member>(
                        _members.Where(m =>
                            m.Name.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            m.Surname.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            m.MembershipCardNumber.ToString().StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }
    }
}

