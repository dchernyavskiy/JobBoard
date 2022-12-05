package com.example.jobboard.data.auth

import com.example.jobboard.domain.repositories.AuthRepository
import net.openid.appauth.AuthorizationRequest
import net.openid.appauth.AuthorizationService
import net.openid.appauth.RegistrationRequest
import net.openid.appauth.TokenRequest

class AuthRepositoryImpl : AuthRepository {

    override fun corruptAccessToken() {
        TokenStorage.accessToken = "fake token"
    }

    override fun getAuthRequest(): AuthorizationRequest {
        return AppAuth.getAuthRequest()
    }

    override fun getRegisterAsEmployerRequest(): RegistrationRequest {
        return AppAuth.getRegisterAsEmployerRequest()
    }

    override fun getRegisterAsEmployeeRequest(): RegistrationRequest {
        return AppAuth.getRegisterAsEmployeeRequest()
    }

    override suspend fun performTokenRequest(
        authService: AuthorizationService,
        tokenRequest: TokenRequest,
    ) {
        val tokens = AppAuth.performTokenRequestSuspend(authService, tokenRequest)
        //обмен кода на токен произошел успешно, сохраняем токены и завершаем авторизацию
        TokenStorage.accessToken = tokens.accessToken
        TokenStorage.refreshToken = tokens.refreshToken
        TokenStorage.idToken = tokens.idToken
    }
}