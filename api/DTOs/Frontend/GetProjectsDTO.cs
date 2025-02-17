﻿using api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Frontend
{
    public class GetProjectsDTO
    {
        public int Project_ID { get; set; }
        public string? Project_Title { get; set; }
        public string? Project_About { get; set; }
        public string? Project_GithubUrl { get; set; }
        public string? Project_LiveUrl { get; set; }
        public string? Project_StartDate { get; set; }
        public string? Project_EndDate { get; set; }
        public bool Project_IsActive { get; set; }
        public string? User_ID { get; set; }
        public int ProjectCategory_ID { get; set; }
        public string? Project_Photo { get; set; }

        //public IEnumerable<GetProjectTechnologiesDTO> Technologies { get; set; }

    }
}
