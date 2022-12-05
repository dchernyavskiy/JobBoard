package com.example.jobboard.ui.auth

import android.app.Application
import android.content.Intent
import androidx.browser.customtabs.CustomTabsIntent
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.jobboard.data.auth.TokensModel
import com.example.jobboard.domain.repositories.AuthRepository
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.channels.trySendBlocking
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import net.openid.appauth.AuthorizationService
import net.openid.appauth.TokenRequest

class StartViewModel(
    private val application: Application,
    private val authRepository: AuthRepository
) : ViewModel() {

    private val authService: AuthorizationService = AuthorizationService(application)

    private val registrationService: AuthorizationService = AuthorizationService(application)

    private val openAuthPageEventChannel = Channel<Intent>(Channel.BUFFERED)
    val openAuthPageFlow: Flow<Intent>
        get() = openAuthPageEventChannel.receiveAsFlow()

    private val authSuccessEventChannel = Channel<Unit>(Channel.BUFFERED)
    val authSuccessFlow: Flow<Unit>
        get() = authSuccessEventChannel.receiveAsFlow()

    private val _loadingLiveData = MutableLiveData(false)
    val loadingLiveData: LiveData<Boolean>
        get() = _loadingLiveData

    fun onAuthCodeReceived(tokenRequest: TokenRequest) {
        viewModelScope.launch {
            _loadingLiveData.value = true
            runCatching {
                authRepository.performTokenRequest(
                    authService = authService,
                    tokenRequest = tokenRequest
                )
            }.onSuccess {
                _loadingLiveData.value = false
                authSuccessEventChannel.send(Unit)
            }.onFailure {
                _loadingLiveData.value = false
            }
        }
    }

    fun openLoginPage() {
        val customTabsIntent = CustomTabsIntent.Builder().build()

        val authRequest = authRepository.getAuthRequest()

        val openAuthPageIntent = authService.getAuthorizationRequestIntent(
            authRequest,
            customTabsIntent
        )

        openAuthPageEventChannel.trySendBlocking(openAuthPageIntent)
    }

//    fun openRegisterEmployerPage() {
//        val customTabsIntent = CustomTabsIntent.Builder().build()
//
//        val registerRequest = authRepository.getRegisterAsEmployerRequest()
//
//        val openAuthPageIntent = authService.performRegistrationRequest(
//            registerRequest
//        ) { response, ex ->
//            when {
//                response != null -> {
//                    val tokens = TokensModel(
//                        accessToken = response.accessToken.orEmpty(),
//                        refreshToken = response.refreshToken.orEmpty(),
//                        idToken = response.idToken.orEmpty()
//                    )
//                    continuation.resumeWith(Result.success(tokens))
//                }
//                ex != null -> {
//                    continuation.resumeWith(Result.failure(ex))
//                }
//                else -> error("unreachable")
//            }
//        }
//
//        openAuthPageEventChannel.trySendBlocking(openAuthPageIntent)
//    }
//
//    fun openRegisterEmployeePage() {
//        val customTabsIntent = CustomTabsIntent.Builder().build()
//
//        val registerRequest = authRepository.getRegisterAsEmployeeRequest()
//
//        val openAuthPageIntent = authService.performRegistrationRequest(
//            registerRequest
//        ) { response, ex ->
//            when {
//                response != null -> {
//                    val tokens = TokensModel(
//                        accessToken = response.accessToken.orEmpty(),
//                        refreshToken = response.refreshToken.orEmpty(),
//                        idToken = response.idToken.orEmpty()
//                    )
//                    continuation.resumeWith(Result.success(tokens))
//                }
//                ex != null -> {
//                    continuation.resumeWith(Result.failure(ex))
//                }
//                else -> error("unreachable")
//            }
//        }
//
//        openAuthPageEventChannel.trySendBlocking(openAuthPageIntent)
//    }
}