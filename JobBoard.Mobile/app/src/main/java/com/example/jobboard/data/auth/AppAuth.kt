package com.example.jobboard.data.auth

import android.net.Uri
import android.util.Log
import androidx.core.net.toUri
import net.openid.appauth.*
import java.util.*
import kotlin.coroutines.suspendCoroutine

object AppAuth {

    private val serviceAuthConfiguration = AuthorizationServiceConfiguration(
        Uri.parse(AuthConfig.AUTH_URI),
        Uri.parse(AuthConfig.TOKEN_URI)
    )

    private val serviceRegisterEmployerConfiguration = AuthorizationServiceConfiguration(
        Uri.parse(AuthConfig.AUTH_URI),
        Uri.parse(AuthConfig.TOKEN_URI),
        Uri.parse(AuthConfig.REGISTER_EMPLOYER_URI)
    )

    private val serviceRegisterEmployeeConfiguration = AuthorizationServiceConfiguration(
        Uri.parse(AuthConfig.AUTH_URI),
        Uri.parse(AuthConfig.REGISTER_EMPLOYEE_URI)
    )

    fun getAuthRequest(): AuthorizationRequest {
        val redirectUri = AuthConfig.CALLBACK_URI.toUri()

        return AuthorizationRequest.Builder(
            serviceAuthConfiguration,
            AuthConfig.CLIENT_ID,
            AuthConfig.RESPONSE_TYPE,
            redirectUri
        )
            .setScope(AuthConfig.SCOPE)
            .setRedirectUri(AuthConfig.CALLBACK_URI.toUri())
            .build()
    }

    fun getRegisterAsEmployerRequest(): RegistrationRequest {
        val redirectUri = AuthConfig.CALLBACK_URI.toUri()

        return RegistrationRequest.Builder(
            serviceRegisterEmployerConfiguration,
            listOf(redirectUri)
        )
            .build()
    }

    fun getRegisterAsEmployeeRequest(): RegistrationRequest {
        val redirectUri = AuthConfig.CALLBACK_URI.toUri()

        return RegistrationRequest.Builder(
            serviceRegisterEmployeeConfiguration,
            listOf(redirectUri)
        )
            .build()
    }

    suspend fun performTokenRequestSuspend(
        authService: AuthorizationService,
        tokenRequest: TokenRequest,
    ): TokensModel {
        return suspendCoroutine { continuation ->
            authService.performTokenRequest(
                tokenRequest
            ) { response, ex ->
                when {
                    response != null -> {
                        val tokens = TokensModel(
                            accessToken = response.accessToken.orEmpty(),
                            refreshToken = response.refreshToken.orEmpty(),
                            idToken = response.idToken.orEmpty()
                        )
                        Log.d("token: ", response.accessToken.toString())
                        continuation.resumeWith(Result.success(tokens))
                    }
                    ex != null -> {
                        continuation.resumeWith(Result.failure(ex))
                    }
                    else -> error("unreachable")
                }
            }
        }
    }

    private object AuthConfig {
        const val BASE_URL = "http://192.168.1.104:45455";
        const val AUTH_URI = "$BASE_URL/auth/login"
        const val REGISTER_EMPLOYER_URI = "$BASE_URL/auth/registeremployer"
        const val REGISTER_EMPLOYEE_URI = "$BASE_URL/auth/registeremployee"
        const val TOKEN_URI = "$BASE_URL"
        const val RESPONSE_TYPE = ResponseTypeValues.CODE
        const val CLIENT_ID = "job-board-android-app"
        const val SCOPE = "openid profile offline_access JobBoardWebApi"
        const val CALLBACK_URI = "com.example.jobboard://oidccallback"
    }
}