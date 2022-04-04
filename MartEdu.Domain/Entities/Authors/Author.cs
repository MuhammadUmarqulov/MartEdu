using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MartEdu.Domain.Entities.Authors
{
    public class Author : IAuditable
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
            
        [JsonIgnore]
        public string Password { get; set; }
        public long Score { get; set; }
        public int CountOfVotes { get; set; }
        public string ProfileImage { get; set; }
        public string BackgroundImage { get; set; }
        public IEnumerable<Course> Courses { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public ItemState State { get; set; }


        public void Create()
        {
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Created;
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Updated;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }
    }
}
