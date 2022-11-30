package com.example.jobboard.ui.main.jobsearch.jobsearch

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import com.example.jobboard.databinding.FragmentJobSearchBinding
import com.example.jobboard.ui.main.jobsearch.jobsearch.adapter.JobAdapter
import org.koin.androidx.viewmodel.ext.android.viewModel

class JobSearchFragment : Fragment() {

    private lateinit var binding: FragmentJobSearchBinding

    private val viewModel by viewModel<JobSearchViewModel>()

    private lateinit var adapter: JobAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentJobSearchBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        adapter = JobAdapter()
        binding.rvJobs.adapter = adapter
        adapter.submitList(viewModel.getAllJobs())
    }
}