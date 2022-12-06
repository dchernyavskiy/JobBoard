package com.example.jobboard.domain.repositories

import net.openid.appauth.AuthorizationRequest
import net.openid.appauth.AuthorizationService
import net.openid.appauth.RegistrationRequest
import net.openid.appauth.TokenRequest

interface AuthRepository {

    fun corruptAccessToken()

    fun getAuthRequest(): AuthorizationRequest

    fun getRegisterAsEmployerRequest(): RegistrationRequest

    fun getRegisterAsEmployeeRequest(): RegistrationRequest

    suspend fun performTokenRequest(
        authService: AuthorizationService,
        tokenRequest: TokenRequest,
    )
}