﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Learning.Data.Entities;
using System.Net.Http;
using Learning.DAL;
using System.Web.Http.Routing;
using Learning.Web.Models;


namespace Learning.Web.Models
{
    public class ModelFactory
    {

        private System.Web.Http.Routing.UrlHelper _UrlHelper;

        private ILearningRepository _repo;

        public ModelFactory(HttpRequestMessage request, ILearningRepository repo)
        {
            _UrlHelper = new UrlHelper(request);
            _repo = repo;
        }
        public CourseModel Create(Course course)
        {
            return new CourseModel()
            {
                Id = course.Id,
                Name = course.Name,
                Duration = course.Duration,
                Description = course.Description,
                Tutor = Create(course.CourseTutor),
                Subject = Create(course.CourseSubject)
            };
        }

        public TutorModel Create(Tutor tutor)
        {
            return new TutorModel()
            {
                Id = tutor.Id,
                Email = tutor.Email,
                UserName = tutor.UserName,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Gender = tutor.Gender
            };
        }

        public SubjectModel Create(Subject subject)
        {
            return new SubjectModel()
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        public EnrollmentModel Create(Enrollment enrollment)
        {
            return new EnrollmentModel()
            {
                EnrollmentDate = enrollment.EnrollmentDate,
                Course = Create(enrollment.Course)
            };
        }

        public Course Parse(CourseModel model)
        {
            try
            {
                var course = new Course()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Duration = model.Duration,
                    CourseSubject = _repo.GetSubject(model.Subject.Id),
                    CourseTutor = _repo.GetTutor(model.Tutor.Id)
                };

                return course;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public StudentModel Create(Student student)
        {
            return new StudentModel()
            {
                Url = _UrlHelper.Link("Students", new { userName = student.UserName }),
                Id = student.Id,
                Email = student.Email,
                UserName = student.UserName,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                RegistrationDate = student.RegistrationDate.Value,
                EnrollmentsCount = student.Enrollments.Count(),
                Enrollments = student.Enrollments.Select(e => Create(e))
            };
        }

    }
}