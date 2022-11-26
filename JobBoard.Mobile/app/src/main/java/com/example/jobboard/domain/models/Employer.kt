package com.example.jobboard.domain.models

data class Employer(
    val id: String,
    val name: String,
    val aboutUs: String,
    val teamSize: String,
    val location: Location,
    val photoUrl: String,
    val jobs: List<Job>
)