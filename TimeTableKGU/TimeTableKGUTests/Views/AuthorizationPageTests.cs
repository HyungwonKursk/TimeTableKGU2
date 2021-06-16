using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTableKGU.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableKGU.Views.Tests
{
    [TestClass()]
    public class AuthorizationPageTests
    {
        [TestMethod()]
        public void GetGroupPageTest()
        {
            AuthorizationPage page = new AuthorizationPage();
            page.GetLoginPage();
        }
        [TestMethod()]
        public void GetClientPageTest()
        {
            AuthorizationPage page = new AuthorizationPage();
            page.GetClientPage();
        }

        [TestMethod()]
        public void GetLoginPageTest()
        {
            AuthorizationPage page = new AuthorizationPage();
            page.GetLoginPage();
        }

        [TestMethod()]
        public void GetRegisPageTest()
        {
            AuthorizationPage page = new AuthorizationPage();
            page.GetRegistrationPage();
        }

        [TestMethod()]
        public void SetChangesPageTest()
        {
            ChangesPage page = new ChangesPage();
            page.SetContent();
        }
    }
}