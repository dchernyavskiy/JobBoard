package com.example.jobboard.domain.repositories

import com.example.jobboard.data.api.models.JobApiModel
import com.example.jobboard.domain.models.Job
import com.example.jobboard.domain.models.api.FilterQuery

interface JobRepository {

    suspend fun getAllJobs(): List<JobApiModel>

    suspend fun getJobsByFilters(filters: FilterQuery): List<JobApiModel>
}