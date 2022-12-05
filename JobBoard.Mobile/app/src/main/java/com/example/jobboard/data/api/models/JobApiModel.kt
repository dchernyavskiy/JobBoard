package com.example.jobboard.data.api.models

import com.google.gson.annotations.SerializedName

data class JobListApiModel(
    @SerializedName("jobs") val jobs: List<JobApiModel>
)

data class JobApiModel(
    @SerializedName("id") val id: String,
    @SerializedName("name") val name: String,
    @SerializedName("location") val location: LocationApiModel,
    @SerializedName("datePosted") val datePosted: String,
    @SerializedName("employment") val employment: String,
    @SerializedName("shortDiscription") val shortDescription: String,
    @SerializedName("category") val category: CategoryApiModel,
)