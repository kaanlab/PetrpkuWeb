using MatBlazor;
using Microsoft.AspNetCore.Components;
using PetrpkuWeb.Shared.Contracts.V1;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using PetrpkuWeb.Shared.Views;

namespace PetrpkuWeb.Client.Pages.AdminRegion
{
    public partial class CatalogManager
    {
        [Inject]
        public IMatToaster Toaster { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        DepartmentView NewDepartment { get; set; } = new DepartmentView();
        BuildingAppUserView NewBuilding { get; set; } = new BuildingAppUserView();
        SiteSectionView NewSiteSection { get; set; } = new SiteSectionView();
        SiteSubSectionView NewSiteSubSection { get; set; } = new SiteSubSectionView();
        CssTypeView NewCssType { get; set; } = new CssTypeView();

        private List<DepartmentView> departments;
        private List<BuildingAppUserView> buildings;
        private List<SiteSectionView> siteSections;
        //private List<SiteSubSectionViewModel> siteSubSections;
        private List<CssTypeView> cssTypes;

        private CssTypeView currentCssType;

        string ListCssClass;

        DepartmentView currentDepartment;
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

        BuildingAppUserView currentBuilding;
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

        SiteSectionView currentSiteSection;
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

        SiteSubSectionView currentSiteSubSection;
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
            departments = await HttpClient.GetJsonAsync<List<DepartmentView>>(ApiRoutes.Departments.ALL);
            buildings = await HttpClient.GetJsonAsync<List<BuildingAppUserView>>(ApiRoutes.Buildings.ALL);
            siteSections = await HttpClient.GetJsonAsync<List<SiteSectionView>>(ApiRoutes.Sections.ALL_INCLUDE_SUBSECTIONS);
            cssTypes = await HttpClient.GetJsonAsync<List<CssTypeView>>(ApiRoutes.CssType.ALL);
        }

        #region Department
        async Task AddNewDepartment()
        {
            newDepartmentDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<DepartmentView>(ApiRoutes.Departments.CREATE, NewDepartment);
            departments.Add(response);
            NewDepartment = new DepartmentView();

            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
        }

        void OpenEditDepartmentDialog(DepartmentView department)
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
            await HttpClient.PutJsonAsync<DepartmentView>($"{ApiRoutes.Departments.UPDATE}/{currentDepartment.DepartmentId}", currentDepartment);
            currentDepartment = null;

            Toaster.Add($"Информация о подразделении успешно обновлена", MatToastType.Success, "Успех!");
        }

        async Task DeleteDepartment()
        {
            editDepartmentDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Departments.DELETE}/{currentDepartment.DepartmentId}");
            departments.Remove(currentDepartment);
            currentDepartment = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }
        #endregion

        #region Building
        async Task AddNewBuilding()
        {
            newBuildingDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<BuildingAppUserView>(ApiRoutes.Buildings.CREATE, NewBuilding);
            buildings.Add(response);
            NewBuilding = new BuildingAppUserView();

            Toaster.Add($"Информация о новом здании успешно добавлена", MatToastType.Success, "Успех!");
        }

        void OpenEditBuildingDialog(BuildingAppUserView building)
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
            await HttpClient.PutJsonAsync<BuildingAppUserView>($"{ApiRoutes.Buildings.UPDATE}/{currentBuilding.BuildingId}", currentBuilding);
            currentBuilding = null;

            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
        }

        async Task DeleteBuilding()
        {
            editBuildingDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Buildings.DELETE}/{currentBuilding.BuildingId}");
            buildings.Remove(currentBuilding);
            currentBuilding = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }
        #endregion

        #region SiteSection
        async Task AddNewSiteSection()
        {
            newSiteSectionDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<SiteSectionView>(ApiRoutes.Sections.CREATE, NewSiteSection);
            siteSections.Add(response);
            NewSiteSection = new SiteSectionView();

            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
        }

        void CancelUpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection = null;
        }

        void OpenEditSiteSectionDialog(SiteSectionView siteSection)
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
            currentSiteSection = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }

        async Task UpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection.Name = SiteSectionName;
            await HttpClient.PutJsonAsync<SiteSectionView>($"{ApiRoutes.Sections.UPDATE}/{currentSiteSection.SiteSectionId}", currentSiteSection);
            currentSiteSection = null;

            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
        }
        #endregion

        #region SiteSubSection
        async Task AddNewSiteSubSection()
        {
            newSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == int.Parse(dropDownSiteSectionId));
            NewSiteSubSection.SiteSectionView = siteSection;
            var response = await HttpClient.PostJsonAsync<SiteSubSectionView>(ApiRoutes.Sections.SUBSECTION_CREATE, NewSiteSubSection);
            var index = siteSections.FindIndex(s => s.SiteSectionId == siteSection.SiteSectionId);
            siteSections[index].SiteSubsectionsView.Add(response);
            NewSiteSubSection = new SiteSubSectionView();

            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
        }

        void CancelUpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection = null;
        }

        void OpenEditSiteSubSectionDialog(SiteSubSectionView siteSubSection)
        {
            editSiteSubSectionDialogIsOpen = true;
            currentSiteSubSection = siteSubSection;
            SiteSubSectionName = currentSiteSubSection.Title;
        }

        async Task DeleteSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == currentSiteSubSection.SiteSectionView.SiteSectionId);
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.Sections.SUBSECTION_DELETE}/{currentSiteSubSection.SiteSubSectionId}");
            var index = siteSections.FindIndex(s => s.SiteSectionId == NewSiteSubSection.SiteSectionView.SiteSectionId);
            siteSections[index].SiteSubsectionsView.Remove(currentSiteSubSection);
            currentSiteSubSection = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }

        async Task UpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection.Title = SiteSubSectionName;
            await HttpClient.PutJsonAsync<SiteSubSectionView>($"{ApiRoutes.Sections.SUBSECTION_UPDATE}/{currentSiteSubSection.SiteSubSectionId}", currentSiteSubSection);
            currentSiteSubSection = null;

            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
        }
        #endregion

        #region CssType
        async Task AddNewCssType()
        {
            newCssTypeDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<CssTypeView>(ApiRoutes.CssType.CREATE, NewCssType);
            cssTypes.Add(response);
            NewCssType = new CssTypeView();

            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
        }

        void OpenEditCssTypeDialog(CssTypeView cssType)
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
            await HttpClient.PutJsonAsync<CssTypeView>($"{ApiRoutes.CssType.UPDATE}/{currentCssType.CssTypeId}", currentCssType);
            currentCssType = null;

            Toaster.Add($"Информация о CSS типе обновлена", MatToastType.Success, "Успех!");
        }

        async Task DeleteCssType()
        {
            editCssTypeDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"{ApiRoutes.CssType.DELETE}/{currentCssType.CssTypeId}");
            cssTypes.Remove(currentCssType);
            currentCssType = null;

            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
        }
        #endregion
    }
}
