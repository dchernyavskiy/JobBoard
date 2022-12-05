package com.example.jobboard.data

import com.example.jobboard.data.api.JobApi
import com.example.jobboard.data.api.models.JobApiModel
import com.example.jobboard.domain.models.Category
import com.example.jobboard.domain.models.Employer
import com.example.jobboard.domain.models.Job
import com.example.jobboard.domain.models.Location
import com.example.jobboard.domain.models.api.FilterQuery
import com.example.jobboard.domain.repositories.JobRepository
import java.util.*

class JobRepositoryImpl(
    private val jobApi: JobApi
) : JobRepository {
    private val ipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
    private val list = (1..10).map {
        Job(
            id = it.toString(),
            title = "Vacancy #$it",
            description = ipsum,
            shortDescription = ipsum,
            datePosted = Date().time - 1000000,
            location = Location(it.toString(), "City $it"),
            salaryStart = 1000,
            salaryEnd = 1500,
            experience = 1,
            employer = Employer("", "", "", "", Location("", ""), "", listOf()),
            category = Category("", "Category $it")
        )
    }
    override suspend fun getAllJobs(): List<JobApiModel> {
        val request = jobApi.getJobsByFilters(FilterQuery())

        return request.body()?.jobs ?: emptyList()
    }

    override suspend fun getJobsByFilters(filters: FilterQuery): List<JobApiModel> {
        val request = jobApi.getJobsByFilters(filters)

        return request.body()?.jobs ?: emptyList()
    }
}