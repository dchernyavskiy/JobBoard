package com.example.jobboard.domain.models

data class Job(
    val id: String,
    val title: String,
    val description: String,
    val shortDescription: String,
    val datePosted: Long,
    val location: Location,
    val salaryStart: Int,
    val salaryEnd: Int,
    val experience: Int,
    val employer: Employer,
    val category: Category
)