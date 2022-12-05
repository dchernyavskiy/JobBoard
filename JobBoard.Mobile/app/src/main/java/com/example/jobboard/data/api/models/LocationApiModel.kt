package com.example.jobboard.data.api.models

import com.google.gson.annotations.SerializedName

data class LocationApiModel(
    @SerializedName("id") val id: String,
    @SerializedName("city") val city: String
)