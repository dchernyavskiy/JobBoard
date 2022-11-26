package com.example.jobboard.data

import com.example.jobboard.domain.models.Category
import com.example.jobboard.domain.models.Employer
import com.example.jobboard.domain.models.Job
import com.example.jobboard.domain.models.Location
import com.example.jobboard.domain.repositories.JobRepository
import java.util.*

class JobRepositoryImpl : JobRepository {
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
    override fun getAllJobs(): List<Job> {
        return list
    }

    override fun findJobs(keyword: String): List<Job> {
        TODO("Not yet implemented")
    }
}