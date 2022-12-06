package com.example.jobboard.data.auth

data class TokensModel(
    val accessToken: String,
    val refreshToken: String,
    val idToken: String
)