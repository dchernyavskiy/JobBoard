package com.example.jobboard.domain.repositories

import com.example.jobboard.domain.models.EmployeeLogin
import com.example.jobboard.domain.models.EmployeeRegister

interface AuthRepository {

    suspend fun login(employeeSignIn: EmployeeLogin): String

    suspend fun register(employeeSignUp: EmployeeRegister): String

    suspend fun isUserAuthorised(): Boolean
}