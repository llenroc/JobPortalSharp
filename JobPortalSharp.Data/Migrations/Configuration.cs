namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using JobPortalSharp.Data.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.IO;
    using Newtonsoft.Json;

    class SeederJob
    {
        public string id { get; set; }
        public string title { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string how_to_apply { get; set; }
        public string company { get; set; }
        public string company_url { get; set; }
        public string company_logo { get; set; }
        public string url { get; set; }
    }

    class SeederCity
    {
        public string name { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class EnumUtils
    {
        public static T GenerateRandom<T>(Random rnd)
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }
    }

    public sealed class Configuration : DbMigrationsConfiguration<JobPortalSharp.Data.JobPortalSharpDbContext>
    {
        private static string lipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobPortalSharp.Data.JobPortalSharpDbContext context)
        {
            var rnd = new Random();

            string[] roles = new string[] { "Employer", "Applicant", "System", "Administrator" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            if (!context.Users.Any(u => u.UserName == "system@example.com"))
            {
                var user = new ApplicationUser { UserName = "system@example.com", Email = "system@example.com" };
                manager.Create(user, "sl@pSh0ck");
                manager.AddToRole(user.Id, "System");
            }

            if (!context.Users.Any(u => u.UserName == "admin@example.com"))
            {
                var user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                var test = manager.Create(user, "sl@pSh0ck");
                manager.AddToRole(user.Id, "Administrator");
            }

            var systemUserId = context.Users.Single(x => x.UserName == "system@example.com").Id;

            context.EmployerTypes.AddOrUpdate(x => x.Id,
                new EmployerType { Id = 1, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Direct Employer" },
                new EmployerType { Id = 2, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Agency" },
                new EmployerType { Id = 3, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "This Website" });

            context.EmploymentTypes.AddOrUpdate(x => x.Id,
                new EmploymentType { Id = 1, Name = "Full Time" },
                new EmploymentType { Id = 2, Name = "Part Time" },
                new EmploymentType { Id = 3, Name = "Contractual" });
            context.SaveChanges();

            SeedIndustryTables(context, systemUserId);
            SeedCountries(context, rnd, systemUserId);

            if (context.Employers.Count() == 0)
            {
                var citiesPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/seeders/cities.json");
                var jobsPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/seeders/jobs.json");

                using (StreamReader rsJobs = new StreamReader(jobsPath))
                {
                    string jsonJobs = rsJobs.ReadToEnd();
                    var jobs = JsonConvert.DeserializeObject<List<SeederJob>>(jsonJobs);
                    using (StreamReader rsCities = new StreamReader(citiesPath))
                    {
                        string jsonCities = rsCities.ReadToEnd();
                        var cities = JsonConvert.DeserializeObject<List<SeederCity>>(jsonCities);

                        SeedEmployers(context, rnd, systemUserId, jobs, cities);
                    }
                    SeedJobPosts(context, rnd, systemUserId, jobs);
                }
            }

            context.Settings.AddOrUpdate(x => x.Name,
                new Settings { CreatedById = systemUserId, Name = "application.title", Value = "JOB LISTING WEBSITE" },
                new Settings { CreatedById = systemUserId, Name = "application.footer", Value = "Copyright 2017" },
                new Settings { CreatedById = systemUserId, Name = "homepage.welcomemessage", Value = "FIND BETTER" },
                new Settings { CreatedById = systemUserId, Name = "homepage.welcomemessage.subtext", Value = "Find the best jobs, employers, and career advice." },
                new Settings { CreatedById = systemUserId, Name = "homepage.image", Value = "Sample text." },
                new Settings { CreatedById = systemUserId, Name = "homepage.bottomtext", Value = "Sample text." },
                new Settings { CreatedById = systemUserId, Name = "about.text", Value = "" });
        }

        private static void SeedIndustryTables(JobPortalSharpDbContext context, string systemUserId)
        {
            context.IndustryCategories.AddOrUpdate(x => x.Id,
                new IndustryCategory { Id = 10, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Accounting" },
                new IndustryCategory { Id = 20, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Administration & Office Support" },
                new IndustryCategory { Id = 30, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Advertising, Arts & Media" },
                new IndustryCategory { Id = 40, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Banking & Financial Services" },
                new IndustryCategory { Id = 50, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Call Centre & Customer Service" },
                new IndustryCategory { Id = 60, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "CEO & General Management" },
                new IndustryCategory { Id = 70, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Community Services & Development" },
                new IndustryCategory { Id = 80, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Construction" },
                new IndustryCategory { Id = 90, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Consulting & Strategy" },
                new IndustryCategory { Id = 100, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Design & Architecture" },
                new IndustryCategory { Id = 110, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Education & Training" },
                new IndustryCategory { Id = 120, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Engineering" },
                new IndustryCategory { Id = 130, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Farming, Animals & Conservation" },
                new IndustryCategory { Id = 140, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Government & Defence" },
                new IndustryCategory { Id = 150, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Healthcare & Medical" },
                new IndustryCategory { Id = 160, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Hospitality & Tourism" },
                new IndustryCategory { Id = 170, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Human Resources & Recruitment" },
                new IndustryCategory { Id = 180, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Information & Communication Technology" },
                new IndustryCategory { Id = 190, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Insurance & Superannuation" },
                new IndustryCategory { Id = 200, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Legal" },
                new IndustryCategory { Id = 210, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Manufacturing, Transport & Logistics" },
                new IndustryCategory { Id = 220, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Marketing & Communications" },
                new IndustryCategory { Id = 230, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mining, Resources & Energy" },
                new IndustryCategory { Id = 240, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Real Estate & Property" },
                new IndustryCategory { Id = 250, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Retail & Consumer Products" },
                new IndustryCategory { Id = 260, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sales" },
                new IndustryCategory { Id = 270, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Science & Technology" },
                new IndustryCategory { Id = 280, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Self Employment" },
                new IndustryCategory { Id = 290, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Trades & Services" });

            context.Industries.AddOrUpdate(x => x.Id,
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 10, CategoryId = 10, Name = "All Accounting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 20, CategoryId = 10, Name = "Accounts Officers/Clerks" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 30, CategoryId = 10, Name = "Accounts Payable" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 40, CategoryId = 10, Name = "Accounts Receivable/Credit Control" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 50, CategoryId = 10, Name = "Analysis & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 60, CategoryId = 10, Name = "Assistant Accountants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 70, CategoryId = 10, Name = "Audit - External" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 80, CategoryId = 10, Name = "Audit - Internal" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 90, CategoryId = 10, Name = "Bookkeeping & Small Practice Accounting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 100, CategoryId = 10, Name = "Business Services & Corporate Advisory" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 110, CategoryId = 10, Name = "Company Secretaries" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 120, CategoryId = 10, Name = "Compliance & Risk" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 130, CategoryId = 10, Name = "Cost Accounting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 140, CategoryId = 10, Name = "Financial Accounting & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 150, CategoryId = 10, Name = "Financial Managers & Controllers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 160, CategoryId = 10, Name = "Forensic Accounting & Investigation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 170, CategoryId = 10, Name = "Insolvency & Corporate Recovery" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 180, CategoryId = 10, Name = "Inventory & Fixed Assets" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 190, CategoryId = 10, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 200, CategoryId = 10, Name = "Management Accounting & Budgeting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 210, CategoryId = 10, Name = "Payroll" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 220, CategoryId = 10, Name = "Strategy & Planning" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 230, CategoryId = 10, Name = "Systems Accounting & IT Audit" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 240, CategoryId = 10, Name = "Taxation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 250, CategoryId = 10, Name = "Treasury" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 260, CategoryId = 10, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 270, CategoryId = 20, Name = "All Administration & Office Support" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 280, CategoryId = 20, Name = "Administrative Assistants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 290, CategoryId = 20, Name = "Client & Sales Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 300, CategoryId = 20, Name = "Contracts Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 310, CategoryId = 20, Name = "Data Entry & Word Processing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 320, CategoryId = 20, Name = "Office Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 330, CategoryId = 20, Name = "PA, EA & Secretarial" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 340, CategoryId = 20, Name = "Receptionists" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 350, CategoryId = 20, Name = "Records Management & Document Control" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 360, CategoryId = 20, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 370, CategoryId = 30, Name = "All Advertising, Arts & Media" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 380, CategoryId = 30, Name = "Agency Account Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 390, CategoryId = 30, Name = "Art Direction" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 400, CategoryId = 30, Name = "Editing & Publishing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 410, CategoryId = 30, Name = "Event Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 420, CategoryId = 30, Name = "Journalism & Writing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 430, CategoryId = 30, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 440, CategoryId = 30, Name = "Media Strategy, Planning & Buying" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 450, CategoryId = 30, Name = "Performing Arts" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 460, CategoryId = 30, Name = "Photography" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 470, CategoryId = 30, Name = "Programming & Production" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 480, CategoryId = 30, Name = "Promotions" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 490, CategoryId = 30, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 500, CategoryId = 40, Name = "All Banking & Financial Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 510, CategoryId = 40, Name = "Account & Relationship Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 520, CategoryId = 40, Name = "Analysis & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 530, CategoryId = 40, Name = "Banking - Business" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 540, CategoryId = 40, Name = "Banking - Corporate & Institutional" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 550, CategoryId = 40, Name = "Banking - Retail/Branch" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 560, CategoryId = 40, Name = "Client Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 570, CategoryId = 40, Name = "Compliance & Risk" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 580, CategoryId = 40, Name = "Corporate Finance & Investment Banking" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 590, CategoryId = 40, Name = "Credit" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 600, CategoryId = 40, Name = "Financial Planning" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 610, CategoryId = 40, Name = "Funds Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 620, CategoryId = 40, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 630, CategoryId = 40, Name = "Mortgages" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 640, CategoryId = 40, Name = "Settlements" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 650, CategoryId = 40, Name = "Stockbroking & Trading" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 660, CategoryId = 40, Name = "Treasury" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 670, CategoryId = 40, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 680, CategoryId = 50, Name = "All Call Centre & Customer Service" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 690, CategoryId = 50, Name = "Collections" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 700, CategoryId = 50, Name = "Customer Service - Call Centre" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 710, CategoryId = 50, Name = "Customer Service - Customer Facing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 720, CategoryId = 50, Name = "Management & Support" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 730, CategoryId = 50, Name = "Sales - Inbound" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 740, CategoryId = 50, Name = "Sales - Outbound" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 750, CategoryId = 50, Name = "Supervisors/Team Leaders" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 760, CategoryId = 50, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 770, CategoryId = 60, Name = "All CEO & General Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 780, CategoryId = 60, Name = "Board Appointments" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 790, CategoryId = 60, Name = "CEO" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 800, CategoryId = 60, Name = "COO & MD" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 810, CategoryId = 60, Name = "General/Business Unit Manager" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 820, CategoryId = 60, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 830, CategoryId = 70, Name = "All Community Services & Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 840, CategoryId = 70, Name = "Aged & Disability Support" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 850, CategoryId = 70, Name = "Child Welfare, Youth & Family Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 860, CategoryId = 70, Name = "Community Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 870, CategoryId = 70, Name = "Employment Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 880, CategoryId = 70, Name = "Fundraising" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 890, CategoryId = 70, Name = "Housing & Homelessness Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 900, CategoryId = 70, Name = "Indigenous & Multicultural Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 910, CategoryId = 70, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 920, CategoryId = 70, Name = "Volunteer Coordination & Support" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 930, CategoryId = 70, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 940, CategoryId = 80, Name = "All Construction" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 950, CategoryId = 80, Name = "Contracts Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 960, CategoryId = 80, Name = "Estimating" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 970, CategoryId = 80, Name = "Foreperson/Supervisors" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 980, CategoryId = 80, Name = "Health, Safety & Environment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 990, CategoryId = 80, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1000, CategoryId = 80, Name = "Planning & Scheduling" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1010, CategoryId = 80, Name = "Plant & Machinery Operators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1020, CategoryId = 80, Name = "Project Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1030, CategoryId = 80, Name = "Quality Assurance & Control" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1040, CategoryId = 80, Name = "Surveying" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1050, CategoryId = 80, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1060, CategoryId = 90, Name = "All Consulting & Strategy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1070, CategoryId = 90, Name = "Analysts" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1080, CategoryId = 90, Name = "Corporate Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1090, CategoryId = 90, Name = "Environment & Sustainability Consulting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1100, CategoryId = 90, Name = "Management & Change Consulting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1110, CategoryId = 90, Name = "Policy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1120, CategoryId = 90, Name = "Strategy & Planning" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1130, CategoryId = 90, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1140, CategoryId = 100, Name = "All Design & Architecture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1150, CategoryId = 100, Name = "Architectural Drafting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1160, CategoryId = 100, Name = "Architecture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1170, CategoryId = 100, Name = "Fashion & Textile Design" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1180, CategoryId = 100, Name = "Graphic Design" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1190, CategoryId = 100, Name = "Illustration & Animation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1200, CategoryId = 100, Name = "Industrial Design" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1210, CategoryId = 100, Name = "Interior Design" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1220, CategoryId = 100, Name = "Landscape Architecture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1230, CategoryId = 100, Name = "Urban Design & Planning" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1240, CategoryId = 100, Name = "Web & Interaction Design" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1250, CategoryId = 100, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1260, CategoryId = 110, Name = "All Education & Training" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1270, CategoryId = 110, Name = "Childcare & Outside School Hours Care" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1280, CategoryId = 110, Name = "Library Services & Information Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1290, CategoryId = 110, Name = "Management - Schools" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1300, CategoryId = 110, Name = "Management - Universities" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1310, CategoryId = 110, Name = "Management - Vocational" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1320, CategoryId = 110, Name = "Research & Fellowships" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1330, CategoryId = 110, Name = "Student Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1340, CategoryId = 110, Name = "Teaching - Early Childhood" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1350, CategoryId = 110, Name = "Teaching - Primary" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1360, CategoryId = 110, Name = "Teaching - Secondary" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1370, CategoryId = 110, Name = "Teaching - Tertiary" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1380, CategoryId = 110, Name = "Teaching - Vocational" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1390, CategoryId = 110, Name = "Teaching Aides & Special Needs" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1400, CategoryId = 110, Name = "Tutoring" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1410, CategoryId = 110, Name = "Workplace Training & Assessment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1420, CategoryId = 110, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1430, CategoryId = 120, Name = "All Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1440, CategoryId = 120, Name = "Aerospace Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1450, CategoryId = 120, Name = "Automotive Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1460, CategoryId = 120, Name = "Building Services Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1470, CategoryId = 120, Name = "Chemical Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1480, CategoryId = 120, Name = "Civil/Structural Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1490, CategoryId = 120, Name = "Electrical/Electronic Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1500, CategoryId = 120, Name = "Engineering Drafting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1510, CategoryId = 120, Name = "Environmental Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1520, CategoryId = 120, Name = "Field Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1530, CategoryId = 120, Name = "Industrial Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1540, CategoryId = 120, Name = "Maintenance" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1550, CategoryId = 120, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1560, CategoryId = 120, Name = "Materials Handling Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1570, CategoryId = 120, Name = "Mechanical Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1580, CategoryId = 120, Name = "Process Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1590, CategoryId = 120, Name = "Project Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1600, CategoryId = 120, Name = "Project Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1610, CategoryId = 120, Name = "Supervisors" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1620, CategoryId = 120, Name = "Systems Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1630, CategoryId = 120, Name = "Water & Waste Engineering" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1640, CategoryId = 120, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1650, CategoryId = 130, Name = "All Farming, Animals & Conservation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1660, CategoryId = 130, Name = "Agronomy & Farm Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1670, CategoryId = 130, Name = "Conservation, Parks & Wildlife" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1680, CategoryId = 130, Name = "Farm Labour" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1690, CategoryId = 130, Name = "Farm Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1700, CategoryId = 130, Name = "Fishing & Aquaculture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1710, CategoryId = 130, Name = "Horticulture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1720, CategoryId = 130, Name = "Veterinary Services & Animal Welfare" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1730, CategoryId = 130, Name = "Winery & Viticulture" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1740, CategoryId = 130, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1750, CategoryId = 140, Name = "All Government & Defence" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1760, CategoryId = 140, Name = "Air Force" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1770, CategoryId = 140, Name = "Army" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1780, CategoryId = 140, Name = "Emergency Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1790, CategoryId = 140, Name = "Navy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1800, CategoryId = 140, Name = "Police & Corrections" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1810, CategoryId = 140, Name = "Policy, Planning & Regulation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1820, CategoryId = 140, Name = "Government - Federal" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1830, CategoryId = 140, Name = "Government - Local" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1840, CategoryId = 140, Name = "Government - State" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1850, CategoryId = 140, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1860, CategoryId = 150, Name = "All Healthcare & Medical" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1870, CategoryId = 150, Name = "Ambulance/Paramedics" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1880, CategoryId = 150, Name = "Chiropractic & Osteopathic" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1890, CategoryId = 150, Name = "Clinical/Medical Research" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1900, CategoryId = 150, Name = "Dental" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1910, CategoryId = 150, Name = "Dieticians" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1920, CategoryId = 150, Name = "Environmental Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1930, CategoryId = 150, Name = "General Practitioners" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1940, CategoryId = 150, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1950, CategoryId = 150, Name = "Medical Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1960, CategoryId = 150, Name = "Medical Imaging" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1970, CategoryId = 150, Name = "Medical Specialists" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1980, CategoryId = 150, Name = "Natural Therapies & Alternative Medicine" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 1990, CategoryId = 150, Name = "Nursing - A&E, Critical Care & ICU" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2000, CategoryId = 150, Name = "Nursing - Aged Care" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2010, CategoryId = 150, Name = "Nursing - Community, Maternal & Child Health" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2020, CategoryId = 150, Name = "Nursing - Educators & Facilitators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2030, CategoryId = 150, Name = "Nursing - General Medical & Surgical" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2040, CategoryId = 150, Name = "Nursing - High Acuity" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2050, CategoryId = 150, Name = "Nursing - Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2060, CategoryId = 150, Name = "Nursing - Midwifery, Neo-Natal, SCN & NICU" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2070, CategoryId = 150, Name = "Nursing - Paediatric & PICU" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2080, CategoryId = 150, Name = "Nursing - Psych, Forensic & Correctional Health" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2090, CategoryId = 150, Name = "Nursing - Theatre & Recovery" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2100, CategoryId = 150, Name = "Optical" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2110, CategoryId = 150, Name = "Pathology" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2120, CategoryId = 150, Name = "Pharmaceuticals & Medical Devices" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2130, CategoryId = 150, Name = "Pharmacy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2140, CategoryId = 150, Name = "Physiotherapy, OT & Rehabilitation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2150, CategoryId = 150, Name = "Psychology, Counselling & Social Work" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2160, CategoryId = 150, Name = "Residents & Registrars" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2170, CategoryId = 150, Name = "Sales" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2180, CategoryId = 150, Name = "Speech Therapy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2190, CategoryId = 150, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2200, CategoryId = 160, Name = "All Hospitality & Tourism" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2210, CategoryId = 160, Name = "Airlines" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2220, CategoryId = 160, Name = "Bar & Beverage Staff" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2230, CategoryId = 160, Name = "Chefs/Cooks" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2240, CategoryId = 160, Name = "Front Office & Guest Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2250, CategoryId = 160, Name = "Gaming" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2260, CategoryId = 160, Name = "Housekeeping" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2270, CategoryId = 160, Name = "Kitchen & Sandwich Hands" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2280, CategoryId = 160, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2290, CategoryId = 160, Name = "Reservations" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2300, CategoryId = 160, Name = "Tour Guides" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2310, CategoryId = 160, Name = "Travel Agents/Consultants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2320, CategoryId = 160, Name = "Waiting Staff" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2330, CategoryId = 160, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2340, CategoryId = 170, Name = "All Human Resources & Recruitment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2350, CategoryId = 170, Name = "Consulting & Generalist HR" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2360, CategoryId = 170, Name = "Industrial & Employee Relations" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2370, CategoryId = 170, Name = "Management - Agency" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2380, CategoryId = 170, Name = "Management - Internal" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2390, CategoryId = 170, Name = "Occupational Health & Safety" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2400, CategoryId = 170, Name = "Organisational Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2410, CategoryId = 170, Name = "Recruitment - Agency" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2420, CategoryId = 170, Name = "Recruitment - Internal" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2430, CategoryId = 170, Name = "Remuneration & Benefits" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2440, CategoryId = 170, Name = "Training & Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2450, CategoryId = 170, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2460, CategoryId = 180, Name = "All Information & Communication Technology" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2470, CategoryId = 180, Name = "Architects" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2480, CategoryId = 180, Name = "Business/Systems Analysts" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2490, CategoryId = 180, Name = "Computer Operators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2500, CategoryId = 180, Name = "Consultants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2510, CategoryId = 180, Name = "Database Development & Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2520, CategoryId = 180, Name = "Developers/Programmers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2530, CategoryId = 180, Name = "Engineering - Hardware" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2540, CategoryId = 180, Name = "Engineering - Network" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2550, CategoryId = 180, Name = "Engineering - Software" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2560, CategoryId = 180, Name = "Help Desk & IT Support" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2570, CategoryId = 180, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2580, CategoryId = 180, Name = "Networks & Systems Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2590, CategoryId = 180, Name = "Product Management & Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2600, CategoryId = 180, Name = "Programme & Project Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2610, CategoryId = 180, Name = "Sales - Pre & Post" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2620, CategoryId = 180, Name = "Security" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2630, CategoryId = 180, Name = "Team Leaders" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2640, CategoryId = 180, Name = "Technical Writing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2650, CategoryId = 180, Name = "Telecommunications" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2660, CategoryId = 180, Name = "Testing & Quality Assurance" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2670, CategoryId = 180, Name = "Web Development & Production" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2680, CategoryId = 180, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2690, CategoryId = 190, Name = "All Insurance & Superannuation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2700, CategoryId = 190, Name = "Actuarial" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2710, CategoryId = 190, Name = "Assessment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2720, CategoryId = 190, Name = "Brokerage" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2730, CategoryId = 190, Name = "Claims" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2740, CategoryId = 190, Name = "Fund Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2750, CategoryId = 190, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2760, CategoryId = 190, Name = "Risk Consulting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2770, CategoryId = 190, Name = "Superannuation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2780, CategoryId = 190, Name = "Underwriting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2790, CategoryId = 190, Name = "Workers' Compensation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2800, CategoryId = 190, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2810, CategoryId = 200, Name = "All Legal" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2820, CategoryId = 200, Name = "Banking & Finance Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2830, CategoryId = 200, Name = "Construction Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2840, CategoryId = 200, Name = "Corporate & Commercial Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2850, CategoryId = 200, Name = "Criminal & Civil Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2860, CategoryId = 200, Name = "Environment & Planning Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2870, CategoryId = 200, Name = "Family Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2880, CategoryId = 200, Name = "Generalists - In-house" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2890, CategoryId = 200, Name = "Generalists - Law Firm" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2900, CategoryId = 200, Name = "Industrial Relations & Employment Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2910, CategoryId = 200, Name = "Insurance & Superannuation Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2920, CategoryId = 200, Name = "Intellectual Property Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2930, CategoryId = 200, Name = "Law Clerks & Paralegals" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2940, CategoryId = 200, Name = "Legal Practice Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2950, CategoryId = 200, Name = "Legal Secretaries" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2960, CategoryId = 200, Name = "Litigation & Dispute Resolution" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2970, CategoryId = 200, Name = "Personal Injury Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2980, CategoryId = 200, Name = "Property Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 2990, CategoryId = 200, Name = "Tax Law" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3000, CategoryId = 200, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3010, CategoryId = 210, Name = "All Manufacturing, Transport & Logistics" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3020, CategoryId = 210, Name = "Analysis & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3030, CategoryId = 210, Name = "Assembly & Process Work" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3040, CategoryId = 210, Name = "Aviation Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3050, CategoryId = 210, Name = "Couriers, Drivers & Postal Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3060, CategoryId = 210, Name = "Fleet Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3070, CategoryId = 210, Name = "Freight/Cargo Forwarding" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3080, CategoryId = 210, Name = "Import/Export & Customs" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3090, CategoryId = 210, Name = "Machine Operators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3100, CategoryId = 210, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3110, CategoryId = 210, Name = "Pattern Makers & Garment Technicians" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3120, CategoryId = 210, Name = "Pickers & Packers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3130, CategoryId = 210, Name = "Production, Planning & Scheduling" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3140, CategoryId = 210, Name = "Public Transport & Taxi Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3150, CategoryId = 210, Name = "Purchasing, Procurement & Inventory" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3160, CategoryId = 210, Name = "Quality Assurance & Control" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3170, CategoryId = 210, Name = "Rail & Maritime Transport" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3180, CategoryId = 210, Name = "Road Transport" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3190, CategoryId = 210, Name = "Team Leaders/Supervisors" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3200, CategoryId = 210, Name = "Warehousing, Storage & Distribution" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3210, CategoryId = 210, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3220, CategoryId = 220, Name = "All Marketing & Communications" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3230, CategoryId = 220, Name = "Brand Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3240, CategoryId = 220, Name = "Digital & Search Marketing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3250, CategoryId = 220, Name = "Direct Marketing & CRM" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3260, CategoryId = 220, Name = "Event Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3270, CategoryId = 220, Name = "Internal Communications" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3280, CategoryId = 220, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3290, CategoryId = 220, Name = "Market Research & Analysis" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3300, CategoryId = 220, Name = "Marketing Assistants/Coordinators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3310, CategoryId = 220, Name = "Marketing Communications" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3320, CategoryId = 220, Name = "Product Management & Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3330, CategoryId = 220, Name = "Public Relations & Corporate Affairs" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3340, CategoryId = 220, Name = "Trade Marketing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3350, CategoryId = 220, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3360, CategoryId = 230, Name = "All Mining, Resources & Energy" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3370, CategoryId = 230, Name = "Analysis & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3380, CategoryId = 230, Name = "Health, Safety & Environment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3390, CategoryId = 230, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3400, CategoryId = 230, Name = "Mining - Drill & Blast" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3410, CategoryId = 230, Name = "Mining - Engineering & Maintenance" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3420, CategoryId = 230, Name = "Mining - Exploration & Geoscience" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3430, CategoryId = 230, Name = "Mining - Operations" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3440, CategoryId = 230, Name = "Mining - Processing" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3450, CategoryId = 230, Name = "Natural Resources & Water" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3460, CategoryId = 230, Name = "Oil & Gas - Drilling" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3470, CategoryId = 230, Name = "Oil & Gas - Engineering & Maintenance" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3480, CategoryId = 230, Name = "Oil & Gas - Exploration & Geoscience" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3490, CategoryId = 230, Name = "Oil & Gas - Operations" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3500, CategoryId = 230, Name = "Oil & Gas - Production & Refinement" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3510, CategoryId = 230, Name = "Power Generation & Distribution" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3520, CategoryId = 230, Name = "Surveying" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3530, CategoryId = 230, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3540, CategoryId = 240, Name = "All Real Estate & Property" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3550, CategoryId = 240, Name = "Administration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3560, CategoryId = 240, Name = "Analysts" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3570, CategoryId = 240, Name = "Body Corporate & Facilities Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3580, CategoryId = 240, Name = "Commercial Sales, Leasing & Property Mgmt" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3590, CategoryId = 240, Name = "Residential Leasing & Property Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3600, CategoryId = 240, Name = "Residential Sales" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3610, CategoryId = 240, Name = "Retail & Property Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3620, CategoryId = 240, Name = "Valuation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3630, CategoryId = 240, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3640, CategoryId = 250, Name = "All Retail & Consumer Products" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3650, CategoryId = 250, Name = "Buying" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3660, CategoryId = 250, Name = "Management - Area/Multi-site" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3670, CategoryId = 250, Name = "Management - Department/Assistant" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3680, CategoryId = 250, Name = "Management - Store" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3690, CategoryId = 250, Name = "Merchandisers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3700, CategoryId = 250, Name = "Planning" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3710, CategoryId = 250, Name = "Retail Assistants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3720, CategoryId = 250, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3730, CategoryId = 260, Name = "All Sales" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3740, CategoryId = 260, Name = "Account & Relationship Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3750, CategoryId = 260, Name = "Analysis & Reporting" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3760, CategoryId = 260, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3770, CategoryId = 260, Name = "New Business Development" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3780, CategoryId = 260, Name = "Sales Coordinators" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3790, CategoryId = 260, Name = "Sales Representatives/Consultants" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3800, CategoryId = 260, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3810, CategoryId = 270, Name = "All Science & Technology" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3820, CategoryId = 270, Name = "Biological & Biomedical Sciences" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3830, CategoryId = 270, Name = "Biotechnology & Genetics" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3840, CategoryId = 270, Name = "Chemistry & Physics" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3850, CategoryId = 270, Name = "Environmental, Earth & Geosciences" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3860, CategoryId = 270, Name = "Food Technology & Safety" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3870, CategoryId = 270, Name = "Laboratory & Technical Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3880, CategoryId = 270, Name = "Materials Sciences" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3890, CategoryId = 270, Name = "Mathematics, Statistics & Information Sciences" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3900, CategoryId = 270, Name = "Modelling & Simulation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3910, CategoryId = 270, Name = "Quality Assurance & Control" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3920, CategoryId = 270, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3930, CategoryId = 280, Name = "All Self Employment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3940, CategoryId = 280, Name = "Self Employment" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3950, CategoryId = 280, Name = "Sport & Recreation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3960, CategoryId = 280, Name = "All Sport & Recreation" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3970, CategoryId = 280, Name = "Coaching & Instruction" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3980, CategoryId = 280, Name = "Fitness & Personal Training" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 3990, CategoryId = 280, Name = "Management" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4000, CategoryId = 280, Name = "Other" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4010, CategoryId = 290, Name = "All Trades & Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4020, CategoryId = 290, Name = "Air Conditioning & Refrigeration" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4030, CategoryId = 290, Name = "Automotive Trades" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4040, CategoryId = 290, Name = "Bakers & Pastry Chefs" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4050, CategoryId = 290, Name = "Building Trades" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4060, CategoryId = 290, Name = "Butchers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4070, CategoryId = 290, Name = "Carpentry & Cabinet Making" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4080, CategoryId = 290, Name = "Cleaning Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4090, CategoryId = 290, Name = "Electricians" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4100, CategoryId = 290, Name = "Fitters, Turners & Machinists" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4110, CategoryId = 290, Name = "Floristry" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4120, CategoryId = 290, Name = "Gardening & Landscaping" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4130, CategoryId = 290, Name = "Hair & Beauty Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4140, CategoryId = 290, Name = "Labourers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4150, CategoryId = 290, Name = "Locksmiths" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4160, CategoryId = 290, Name = "Maintenance & Handyperson Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4170, CategoryId = 290, Name = "Nannies & Babysitters" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4180, CategoryId = 290, Name = "Painters & Sign Writers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4190, CategoryId = 290, Name = "Plumbers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4200, CategoryId = 290, Name = "Printing & Publishing Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4210, CategoryId = 290, Name = "Security Services" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4220, CategoryId = 290, Name = "Tailors & Dressmakers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4230, CategoryId = 290, Name = "Technicians" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4240, CategoryId = 290, Name = "Welders & Boilermakers" },
                new Industry { CreatedById = systemUserId, CreatedDate = DateTime.Now, Id = 4250, CategoryId = 290, Name = "Other" });
        }

        private static void SeedEmployers(JobPortalSharpDbContext context, Random rnd, string systemUserId, IList<SeederJob> jobs, IList<SeederCity> cities)
        {
            if (context.Employers.Count() == 0)
            {
                var employerTypes = context.EmployerTypes.ToList();
                for (var i = 0; i < jobs.Count;  i++)
                {
                    var job = jobs[i];
                    if (!context.Employers.Any(x => x.Name == job.company))
                    {
                        var store = new UserStore<ApplicationUser>(context);
                        var manager = new UserManager<ApplicationUser>(store);
                        var user = new ApplicationUser { UserName = "employer" + (i + 1).ToString() + "@example.com", Email = "employer" + (i + 1).ToString() + "@example.com" };
                        manager.Create(user, "sl@pSh0ck");
                        manager.AddToRole(user.Id, "Employer");

                        var city = cities[rnd.Next(0, cities.Count)];
                        context.Employers.Add(new Employer
                        {
                            Name = job.company,
                            CompanyDescription = lipsum,
                            NumberOfEmployees = EnumUtils.GenerateRandom<NumberOfEmployees>(rnd),
                            ApplicationUserId = user.Id,
                            CountryId = 1880,
                            AddressTown = city.name,
                            AddressLatitude = city.lat,
                            AddressLongitude = city.lng,
                            EmployerTypeId = employerTypes[rnd.Next(0, employerTypes.Count)].Id,
                        });
                        context.SaveChanges();
                    }
                }
            }
        }

        private static void SeedJobPosts(JobPortalSharpDbContext context, Random rnd, string systemUserId, IList<SeederJob> jobs)
        {
            if (context.JobPosts.Count() == 0)
            {
                var industries = context.Industries.ToList();
                var employmentTypes = context.EmploymentTypes.ToList();
                var employers = context.Employers.ToList();

                foreach (var job in jobs)
                {
                    context.JobPosts.Add(new JobPost
                    {
                        Name = job.title,
                        Details = job.description,
                        IndustryId = industries[rnd.Next(0, industries.Count)].Id,
                        EmploymentTypeId = employmentTypes[rnd.Next(0, employmentTypes.Count)].Id,
                        EmployerId = context.Employers.Single(x => x.Name == job.company).Id,
                        CreatedDate = DateTime.Now,
                        CreatedById = systemUserId,
                        ExpirationDate = RandomDay(rnd),
                        PostDate = RandomDay(rnd),
                        Paid = true,
                        LocationSameAsEmployer = true
                    });
                }

                context.SaveChanges();
            }
        }

        private static void SeedCountries(JobPortalSharpDbContext context, Random rnd, string systemUserId)
        {
            context.Countries.AddOrUpdate(x => x.Id,
                new Country { Id = 10, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Afghanistan" },
                new Country { Id = 20, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Albania" },
                new Country { Id = 30, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Algeria" },
                new Country { Id = 40, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Andorra" },
                new Country { Id = 50, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Angola" },
                new Country { Id = 60, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Antigua and Barbuda" },
                new Country { Id = 70, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Argentina" },
                new Country { Id = 80, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Armenia" },
                new Country { Id = 90, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Australia" },
                new Country { Id = 100, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Austria" },
                new Country { Id = 110, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Azerbaijan" },
                new Country { Id = 120, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bahamas" },
                new Country { Id = 130, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bahrain" },
                new Country { Id = 140, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bangladesh" },
                new Country { Id = 150, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Barbados" },
                new Country { Id = 160, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Belarus" },
                new Country { Id = 170, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Belgium" },
                new Country { Id = 180, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Belize" },
                new Country { Id = 190, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Benin" },
                new Country { Id = 200, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bhutan" },
                new Country { Id = 210, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bolivia" },
                new Country { Id = 220, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bosnia and Herzegovina" },
                new Country { Id = 230, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Botswana" },
                new Country { Id = 240, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Brazil" },
                new Country { Id = 250, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Brunei" },
                new Country { Id = 260, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Bulgaria" },
                new Country { Id = 270, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Burkina Faso" },
                new Country { Id = 280, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Burundi" },
                new Country { Id = 290, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cabo Verde" },
                new Country { Id = 300, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cambodia" },
                new Country { Id = 310, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cameroon" },
                new Country { Id = 320, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Canada" },
                new Country { Id = 330, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Central African Republic (CAR)" },
                new Country { Id = 340, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Chad" },
                new Country { Id = 350, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Chile" },
                new Country { Id = 360, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "China" },
                new Country { Id = 370, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Colombia" },
                new Country { Id = 380, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Comoros" },
                new Country { Id = 390, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Democratic Republic of the Congo" },
                new Country { Id = 400, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Republic of the Congo" },
                new Country { Id = 410, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Costa Rica" },
                new Country { Id = 420, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cote d'Ivoire" },
                new Country { Id = 430, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Croatia" },
                new Country { Id = 440, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cuba" },
                new Country { Id = 450, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Cyprus" },
                new Country { Id = 460, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Czech Republic" },
                new Country { Id = 470, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Denmark" },
                new Country { Id = 480, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Djibouti" },
                new Country { Id = 490, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Dominica" },
                new Country { Id = 500, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Dominican Republic" },
                new Country { Id = 510, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Ecuador" },
                new Country { Id = 520, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Egypt" },
                new Country { Id = 530, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "El Salvador" },
                new Country { Id = 540, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Equatorial Guinea" },
                new Country { Id = 550, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Eritrea" },
                new Country { Id = 560, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Estonia" },
                new Country { Id = 570, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Ethiopia" },
                new Country { Id = 580, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Fiji" },
                new Country { Id = 590, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Finland" },
                new Country { Id = 600, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "France" },
                new Country { Id = 610, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Gabon" },
                new Country { Id = 620, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Gambia" },
                new Country { Id = 630, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Georgia" },
                new Country { Id = 640, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Germany" },
                new Country { Id = 650, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Ghana" },
                new Country { Id = 660, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Greece" },
                new Country { Id = 670, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Grenada" },
                new Country { Id = 680, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Guatemala" },
                new Country { Id = 690, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Guinea" },
                new Country { Id = 700, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Guinea-Bissau" },
                new Country { Id = 710, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Guyana" },
                new Country { Id = 720, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Haiti" },
                new Country { Id = 730, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Honduras" },
                new Country { Id = 740, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Hungary" },
                new Country { Id = 750, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Iceland" },
                new Country { Id = 760, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "India" },
                new Country { Id = 770, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Indonesia" },
                new Country { Id = 780, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Iran" },
                new Country { Id = 790, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Iraq" },
                new Country { Id = 800, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Ireland" },
                new Country { Id = 810, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Israel" },
                new Country { Id = 820, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Italy" },
                new Country { Id = 830, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Jamaica" },
                new Country { Id = 840, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Japan" },
                new Country { Id = 850, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Jordan" },
                new Country { Id = 860, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kazakhstan" },
                new Country { Id = 870, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kenya" },
                new Country { Id = 880, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kiribati" },
                new Country { Id = 890, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kosovo" },
                new Country { Id = 900, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kuwait" },
                new Country { Id = 910, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Kyrgyzstan" },
                new Country { Id = 920, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Laos" },
                new Country { Id = 930, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Latvia" },
                new Country { Id = 940, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Lebanon" },
                new Country { Id = 950, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Lesotho" },
                new Country { Id = 960, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Liberia" },
                new Country { Id = 970, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Libya" },
                new Country { Id = 980, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Liechtenstein" },
                new Country { Id = 990, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Lithuania" },
                new Country { Id = 1000, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Luxembourg" },
                new Country { Id = 1010, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Macedonia (FYROM)" },
                new Country { Id = 1020, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Madagascar" },
                new Country { Id = 1030, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Malawi" },
                new Country { Id = 1040, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Malaysia" },
                new Country { Id = 1050, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Maldives" },
                new Country { Id = 1060, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mali" },
                new Country { Id = 1070, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Malta" },
                new Country { Id = 1080, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Marshall Islands" },
                new Country { Id = 1090, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mauritania" },
                new Country { Id = 1100, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mauritius" },
                new Country { Id = 1110, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mexico" },
                new Country { Id = 1120, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Micronesia" },
                new Country { Id = 1130, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Moldova" },
                new Country { Id = 1140, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Monaco" },
                new Country { Id = 1150, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mongolia" },
                new Country { Id = 1160, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Montenegro" },
                new Country { Id = 1170, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Morocco" },
                new Country { Id = 1180, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Mozambique" },
                new Country { Id = 1190, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Myanmar (Burma)" },
                new Country { Id = 1200, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Namibia" },
                new Country { Id = 1210, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Nauru" },
                new Country { Id = 1220, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Nepal" },
                new Country { Id = 1230, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Netherlands" },
                new Country { Id = 1240, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "New Zealand" },
                new Country { Id = 1250, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Nicaragua" },
                new Country { Id = 1260, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Niger" },
                new Country { Id = 1270, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Nigeria" },
                new Country { Id = 1280, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "North Korea" },
                new Country { Id = 1290, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Norway" },
                new Country { Id = 1300, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Oman" },
                new Country { Id = 1310, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Pakistan" },
                new Country { Id = 1320, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Palau" },
                new Country { Id = 1330, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Palestine" },
                new Country { Id = 1340, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Panama" },
                new Country { Id = 1350, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Papua New Guinea" },
                new Country { Id = 1360, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Paraguay" },
                new Country { Id = 1370, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Peru" },
                new Country { Id = 1380, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Philippines" },
                new Country { Id = 1390, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Poland" },
                new Country { Id = 1400, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Portugal" },
                new Country { Id = 1410, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Qatar" },
                new Country { Id = 1420, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Romania" },
                new Country { Id = 1430, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Russia" },
                new Country { Id = 1440, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Rwanda" },
                new Country { Id = 1450, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Saint Kitts and Nevis" },
                new Country { Id = 1460, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Saint Lucia" },
                new Country { Id = 1470, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Saint Vincent and the Grenadines" },
                new Country { Id = 1480, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Samoa" },
                new Country { Id = 1490, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "San Marino" },
                new Country { Id = 1500, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sao Tome and Principe" },
                new Country { Id = 1510, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Saudi Arabia" },
                new Country { Id = 1520, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Senegal" },
                new Country { Id = 1530, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Serbia" },
                new Country { Id = 1540, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Seychelles" },
                new Country { Id = 1550, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sierra Leone" },
                new Country { Id = 1560, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Singapore" },
                new Country { Id = 1570, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Slovakia" },
                new Country { Id = 1580, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Slovenia" },
                new Country { Id = 1590, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Solomon Islands" },
                new Country { Id = 1600, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Somalia" },
                new Country { Id = 1610, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "South Africa" },
                new Country { Id = 1620, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "South Korea" },
                new Country { Id = 1630, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "South Sudan" },
                new Country { Id = 1640, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Spain" },
                new Country { Id = 1650, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sri Lanka" },
                new Country { Id = 1660, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sudan" },
                new Country { Id = 1670, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Suriname" },
                new Country { Id = 1680, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Swaziland" },
                new Country { Id = 1690, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Sweden" },
                new Country { Id = 1700, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Switzerland" },
                new Country { Id = 1710, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Syria" },
                new Country { Id = 1720, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Taiwan" },
                new Country { Id = 1730, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Tajikistan" },
                new Country { Id = 1740, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Tanzania" },
                new Country { Id = 1750, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Thailand" },
                new Country { Id = 1760, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Timor-Leste" },
                new Country { Id = 1770, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Togo" },
                new Country { Id = 1780, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Tonga" },
                new Country { Id = 1790, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Trinidad and Tobago" },
                new Country { Id = 1800, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Tunisia" },
                new Country { Id = 1810, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Turkey" },
                new Country { Id = 1820, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Turkmenistan" },
                new Country { Id = 1830, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Tuvalu" },
                new Country { Id = 1840, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Uganda" },
                new Country { Id = 1850, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Ukraine" },
                new Country { Id = 1860, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "United Arab Emirates (UAE)" },
                new Country { Id = 1870, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "United Kingdom (UK)" },
                new Country { Id = 1880, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "United States of America (USA)" },
                new Country { Id = 1890, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Uruguay" },
                new Country { Id = 1900, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Uzbekistan" },
                new Country { Id = 1910, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Vanuatu" },
                new Country { Id = 1920, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Vatican City (Holy See)" },
                new Country { Id = 1930, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Venezuela" },
                new Country { Id = 1940, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Vietnam" },
                new Country { Id = 1950, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Yemen" },
                new Country { Id = 1960, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Zambia" },
                new Country { Id = 1970, CreatedById = systemUserId, CreatedDate = DateTime.Now, Name = "Zimbabwe" });
        }

        private static DateTime RandomDay(Random rnd)
        {
            DateTime start = new DateTime(2017, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
