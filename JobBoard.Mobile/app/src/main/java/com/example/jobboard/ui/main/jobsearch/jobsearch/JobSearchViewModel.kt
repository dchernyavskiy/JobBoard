package com.example.jobboard.ui.main.jobsearch.jobsearch

import androidx.lifecycle.ViewModel
import com.example.jobboard.domain.models.Job
import com.example.jobboard.domain.repositories.JobRepository

class JobSearchViewModel(
    private val jobRepository: JobRepository
) : ViewModel() {

    fun getAllJobs(): List<Job> {
        return jobRepository.getAllJobs()
    }
}