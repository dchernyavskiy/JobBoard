package com.example.jobboard.data.api

import com.example.jobboard.data.api.models.JobApiModel
import com.example.jobboard.data.api.models.JobListApiModel
import com.example.jobboard.domain.models.api.FilterQuery
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface JobApi {
    @POST("api/v$API_VERSION/Job/GetAll")
    suspend fun getJobsByFilters(@Body filterQuery: FilterQuery): Response<JobListApiModel>
}