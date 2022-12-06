package com.example.jobboard.di

import com.example.jobboard.ui.auth.StartViewModel
import com.example.jobboard.ui.main.jobsearch.jobsearch.JobSearchViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val viewModelModule = module {

    viewModel {
        StartViewModel(get(), get())
    }

    viewModel {
        JobSearchViewModel(get())
    }
}