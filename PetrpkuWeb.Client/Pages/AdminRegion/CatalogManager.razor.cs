using MatBlazor;
using Microsoft.AspNetCore.Components;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using PetrpkuWeb.Shared.ViewModels.CatalogRegion;

namespace PetrpkuWeb.Client.Pages.AdminRegion
{
    public partial class CatalogManager
    {
        [Inject]
        public IMatToaster Toaster { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        CatalogDepartmentView NewDepartment { get; set; } = new CatalogDepartmentView();
        BuildingViewModel NewBuilding { get; set; } = new BuildingViewModel();
        SiteSectionViewModel NewSiteSection { get; set; } = new SiteSectionViewModel();
        SiteSubSectionViewModel NewSiteSubSection { get; set; } = new SiteSubSectionViewModel();
        CssTypeViewModel NewCssType { get; set; } = new CssTypeViewModel();
 
        private List<CatalogDepartmentView> departments;
        private List<BuildingViewModel> buildings;
        private List<SiteSectionViewModel> siteSections;
        //private List<SiteSubSectionViewModel> siteSubSections;
        private List<CssTypeViewModel> cssTypes;

        private CssTypeViewModel currentCssType;

        string ListCssClass;

        CatalogDepartmentView currentDepartment;
        private string _departmentName;
        public string DepatrmentName
        {
            get => _departmentName;
            set
            {
                _departmentName = value;
                this.StateHasChanged();
            }
        }

        BuildingViewModel currentBuilding;
        private string _buildingName;
        public string BuildingName
        {
            get => _buildingName;
            set
            {
                _buildingName = value;
                this.StateHasChanged();
            }
        }

        SiteSectionViewModel currentSiteSection;
        private string _siteSectionName;
        public string SiteSectionName
        {
            get => _siteSectionName;
            set
            {
                _siteSectionName = value;
                this.StateHasChanged();
            }
        }

        SiteSubSectionViewModel currentSiteSubSection;
        private string _siteSubSectionName;
        public string SiteSubSectionName
        {
            get => _siteSubSectionName;
            set
            {
                _siteSubSectionName = value;
                this.StateHasChanged();
            }
        }

        string dropDownSiteSectionId;

        private bool newDepartmentDialogIsOpen;
        private bool newBuildingDialogIsOpen;
        private bool newSiteSectionDialogIsOpen;
        private bool newSiteSubSectionDialogIsOpen;
        private bool newCssTypeDialogIsOpen;

        private bool editDepartmentDialogIsOpen;
        private bool editBuildingDialogIsOpen;
        private bool editSiteSectionDialogIsOpen;
        private bool editSiteSubSectionDialogIsOpen;
        private bool editCssTypeDialogIsOpen;


        protected override async Task OnInitializedAsync()
        {
            departments = await HttpClient.GetJsonAsync<List<CatalogDepartmentView>>(ApiRoutes.Departments.ALL);
            buildings = await HttpClient.GetJsonAsync<List<BuildingViewModel>>(ApiRoutes.Buildings.ALL);
            siteSections = await HttpClient.GetJsonAsync<List<SiteSectionViewModel>>(ApiRoutes.Sections.ALL_INCLUDE_SUBSECTIONS);
            cssTypes = await HttpClient.GetJsonAsync<List<CssTypeViewModel>>(ApiRoutes.CssType.ALL);
        }

        #region Department
        async Task AddNewDepartment()
        {
            newDepartmentDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<CatalogDepartmentView>(ApiRoutes.Departments.CREATE, NewDepartment);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            departments.Add(response);
            NewDepartment = new CatalogDepartmentView();
        }

        void OpenEditDepartmentDialog(CatalogDepartmentView department)
        {
            editDepartmentDialogIsOpen = true;
            currentDepartment = department;
            DepatrmentName = currentDepartment.Name;
        }

        void CancelUpdateDepartment()
        {
            editDepartmentDialogIsOpen = false;
            currentDepartment = null;
        }

        async Task UpdateDepartment()
        {
            editDepartmentDialogIsOpen = false;
            currentDepartment.Name = DepatrmentName;
            await HttpClient.PutJsonAsync<DepartmentViewModel>($"{ApiRoutes.Departments.UPDATE}/{currentDepartment.DepartmentId}", currentDepartment);
            Toaster.Add($"Информация о подразделении успешно обновлена", MatToastType.Success, "Успех!");
            currentDepartment = null;
        }

        async Task DeleteDepartment()
        {
            editDepartmentDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Departments.DELETE}/{currentDepartment.DepartmentId}");
            departments.Remove(currentDepartment);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentDepartment = null;
        }
        #endregion

        #region Building
        async Task AddNewBuilding()
        {
            newBuildingDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<BuildingViewModel>(ApiRoutes.Buildings.CREATE, NewBuilding);
            Toaster.Add($"Информация о новом здании успешно добавлена", MatToastType.Success, "Успех!");
            buildings.Add(response);
            NewBuilding = new BuildingViewModel();
        }

        void OpenEditBuildingDialog(BuildingViewModel building)
        {
            editBuildingDialogIsOpen = true;
            currentBuilding = building;
            BuildingName = currentBuilding.Name;
        }

        void CancelUpdateBuilding()
        {
            editBuildingDialogIsOpen = false;
            currentBuilding = null;
        }

        async Task UpdateBuilding()
        {
            editBuildingDialogIsOpen = false;
            currentBuilding.Name = BuildingName;
            await HttpClient.PutJsonAsync<BuildingViewModel>($"{ApiRoutes.Buildings.UPDATE}/{currentBuilding.BuildingId}", currentBuilding);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentBuilding = null;
        }

        async Task DeleteBuilding()
        {
            editBuildingDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Buildings.DELETE}/{currentBuilding.BuildingId}");
            buildings.Remove(currentBuilding);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentBuilding = null;
        }
#endregion

        #region SiteSection
        async Task AddNewSiteSection()
        {
            newSiteSectionDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<SiteSectionViewModel>(ApiRoutes.Sections.CREATE, NewSiteSection);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            siteSections.Add(response);
            NewSiteSection = new SiteSectionViewModel();
        }

        void CancelUpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection = null;
        }

        void OpenEditSiteSectionDialog(SiteSectionViewModel siteSection)
        {
            editSiteSectionDialogIsOpen = true;
            currentSiteSection = siteSection;
            SiteSectionName = currentSiteSection.Name;
        }

        async Task DeleteSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Sections.DELETE}/{currentSiteSection.SiteSectionId}");
            siteSections.Remove(currentSiteSection);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentSiteSection = null;
        }

        async Task UpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection.Name = SiteSectionName;
            await HttpClient.PutJsonAsync<SiteSectionViewModel>($"{ApiRoutes.Sections.UPDATE}/{currentSiteSection.SiteSectionId}", currentSiteSection);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentSiteSection = null;
        }
        #endregion

        #region SiteSubSection
        async Task AddNewSiteSubSection()
        {
            newSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == int.Parse(dropDownSiteSectionId));
            NewSiteSubSection.SiteSectionViewModel = siteSection;
            var response = await HttpClient.PostJsonAsync<SiteSubSectionViewModel>(ApiRoutes.Sections.SUBSECTION_CREATE, NewSiteSubSection);
            var index = siteSections.FindIndex(s => s.SiteSectionId == siteSection.SiteSectionId);
            siteSections[index].SiteSubsectionsViewModel.Add(response);
            NewSiteSubSection = new SiteSubSectionViewModel();

            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
        }

        void CancelUpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection = null;
        }

        void OpenEditSiteSubSectionDialog(SiteSubSectionViewModel siteSubSection)
        {
            editSiteSubSectionDialogIsOpen = true;
            currentSiteSubSection = siteSubSection;
            SiteSubSectionName = currentSiteSubSection.Title;
        }

        async Task DeleteSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == currentSiteSubSection.SiteSectionViewModel.SiteSectionId);
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Sections.SUBSECTION_DELETE}/{currentSiteSubSection.SiteSubsectionId}");
            var index = siteSections.FindIndex(s => s.SiteSectionId == NewSiteSubSection.SiteSectionViewModel.SiteSectionId);
            siteSections[index].SiteSubsectionsViewModel.Remove(currentSiteSubSection);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentSiteSubSection = null;
        }

        async Task UpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection.Title = SiteSubSectionName;
            await HttpClient.PutJsonAsync<SiteSubSectionViewModel>($"{ApiRoutes.Sections.SUBSECTION_UPDATE}/{currentSiteSubSection.SiteSubsectionId}", currentSiteSubSection);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentSiteSubSection = null;
        }
#endregion

        #region CssType
        async Task AddNewCssType()
        {
            newCssTypeDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<CssTypeViewModel>(ApiRoutes.CssType.CREATE, NewCssType);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            cssTypes.Add(response);
            NewCssType = new CssTypeViewModel();
        }

        void OpenEditCssTypeDialog(CssTypeViewModel cssType)
        {
            editCssTypeDialogIsOpen = true;
            currentCssType = cssType;
        }

        void CancelUpdateCssType()
        {
            editCssTypeDialogIsOpen = false;
        }

        async Task UpdateCssType()
        {
            editCssTypeDialogIsOpen = false;
            await HttpClient.PutJsonAsync<CssTypeViewModel>($"{ApiRoutes.CssType.UPDATE}/{currentCssType.CssTypeId}", currentCssType);
            currentCssType = null;

            Toaster.Add($"Информация о CSS типе обновлена", MatToastType.Success, "Успех!");
        }

        async Task DeleteCssType()
        {
            editCssTypeDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"api/csstype/delete/{currentCssType.CssTypeId}");
            cssTypes.Remove(currentCssType);
            currentCssType = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }
        #endregion
    }
}
