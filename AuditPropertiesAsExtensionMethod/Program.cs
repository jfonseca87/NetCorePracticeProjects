using ConsoleApp2.Extensions;
using ConsoleApp2.Models;
using System;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Project newProject = new Project
            {
                Id = 1,
                Name = "Test Project",
                Description = "Test Description",
                Version = "1.0.0",
                Title = "Project Test"
            };
            newProject.SetAudit(true, 1);


            Project updateProject = new Project
            {
                Id = 1,
                Name = "Test Project",
                Description = "Test Description",
                Version = "1.0.0",
                Title = "Project Test"
            };
            updateProject.SetAudit(false, 1);
        }
    }
}
