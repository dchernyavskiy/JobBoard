package com.example.jobboard.data

import com.example.jobboard.domain.models.EmployeeLogin
import com.example.jobboard.domain.models.EmployeeRegister
import com.example.jobboard.domain.repositories.AuthRepository

class AuthRepositoryImpl(

) : AuthRepository {

    override suspend fun login(employeeSignIn: EmployeeLogin): String {
        val email = "admin@gmail.com"
        val password = "123456"
        return if(employeeSignIn.email == email && employeeSignIn.password == password) {
            "login"
        } else {
            throw RuntimeException("Електронна пошта або пароль введені невірно.")
        }
    }

    override suspend fun register(employeeSignUp: EmployeeRegister): String {
       return "registered"
    }

    override suspend fun isUserAuthorised(): Boolean {
        return false
    }
}