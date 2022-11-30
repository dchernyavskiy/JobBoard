package com.example.jobboard.domain.repositories

import com.example.jobboard.domain.models.Job

interface JobRepository {

    fun getAllJobs(): List<Job>

    fun findJobs(keyword: String): List<Job>
}