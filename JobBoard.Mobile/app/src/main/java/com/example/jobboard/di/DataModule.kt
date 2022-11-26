package com.example.jobboard.di

import com.example.jobboard.data.AuthRepositoryImpl
import com.example.jobboard.data.JobRepositoryImpl
import com.example.jobboard.domain.repositories.AuthRepository
import com.example.jobboard.domain.repositories.JobRepository
import org.koin.dsl.module

val dataModule = module {

    factory<AuthRepository> {
        AuthRepositoryImpl()
    }

    factory<JobRepository> {
        JobRepositoryImpl()
    }
}