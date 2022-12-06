package com.example.jobboard.data.api.models

import com.google.gson.annotations.SerializedName

data class CategoryApiModel(
    @SerializedName("id") val id: String,
    @SerializedName("name") val name: String
)