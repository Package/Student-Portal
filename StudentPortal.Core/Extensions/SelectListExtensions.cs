using StudentPortal.Domain.Models;
using StudentPortal.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortal.Core.Extensions
{
	public static class SelectListExtensions
	{
		/// <summary>
		/// Generate a dropdown list of courses to pick from based on the list of available courses and course groups within the database.
		/// Additionally segments the courses into the course groups defined in the database to make courses easier to find.
		/// </summary>
		/// <param name="courses">List of courses to include in the drop down</param>
		/// <param name="groups">List of groups to segment the courses into</param>
		/// <param name="selectedCourseId">Pre-select a course if it has been selected previously</param>
		/// <returns>A dropdown list of available courses to apply for.</returns>
		public static IEnumerable<SelectListItem> ToSelectList(this List<Course> courses, List<CourseGroup> groups, int? selectedCourseId)
		{
			// Order the courses into groups in the select list, following the same groups as the overall course groups.
			List<SelectListGroup> selectGroups = new List<SelectListGroup>();
			if (groups != null && groups.Any())
			{
				groups.ForEach(g => selectGroups.Add(new SelectListGroup { Name = g.Name }));
			}

			// Convert each course into an item in the select list
			foreach (Course course in courses)
			{
				// Find the group that this course belongs to, if any.
				CourseGroup courseGroup = groups.FirstOrDefault(g => g.Id == course.Type);
				SelectListGroup selectGroup = selectGroups.FirstOrDefault(g => courseGroup != null && g.Name == courseGroup.Name);

				yield return new SelectListItem {
					Value = course.Id.ToString(),
					Text = course.Title,
					Selected = selectedCourseId.HasValue ? selectedCourseId.Value == course.Id : false,
					Group = selectGroup
				};
			}
		}
		
		/// <summary>
		/// Generate a dropdown list of ISelectable items.
		/// </summary>
		/// <param name="types"></param>
		/// <param name="selectedValue"></param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> ToSelectList(this List<ISelectable> types, string selectedValue)
		{
			foreach (ISelectable type in types)
			{
				yield return new SelectListItem
				{
					Text = type.Name,
					Value = type.Id.ToString(),
					Selected = selectedValue != null ? selectedValue == type.Id.ToString() : false
				};
			}
		}
		
		/// <summary>
		/// Generate a select list of Qualification Types for the user to pick from when entering their qualifications.
		/// </summary>
		/// <param name="types"></param>
		/// <param name="selectedType"></param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> ToQualificationTypeSelectList(this List<QualificationType> types, string selectedType)
		{
			foreach (QualificationType type in types)
			{
				yield return new SelectListItem
				{
					Text = type.Name,
					Value = type.Name,
					Selected = selectedType != null ? selectedType == type.Name : false
				};
			}
		}
	}
}