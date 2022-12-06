package com.example.jobboard.domain.models

data class EmployeeRegister(
    val email: String,
    val name: String,
    val password: String,
    val country: String,
    val state: String,
    val city: String
)