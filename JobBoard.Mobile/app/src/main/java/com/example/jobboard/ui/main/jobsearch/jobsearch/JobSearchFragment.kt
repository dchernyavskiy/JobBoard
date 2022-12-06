package com.example.jobboard.ui.main.jobsearch.jobsearch

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.jobboard.R
import com.example.jobboard.databinding.BottomSheetSortBinding
import com.example.jobboard.databinding.FragmentJobSearchBinding
import com.example.jobboard.ui.main.jobsearch.jobsearch.adapter.JobAdapter
import com.google.android.material.bottomsheet.BottomSheetDialog
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

        binding.sortLayout.setOnClickListener {
            showSortBottomSheet()
        }

        setupObservers()
        viewModel.getAllJobs()
    }

    private fun showSortBottomSheet() {
        val sortBinding = BottomSheetSortBinding.inflate(
            LayoutInflater.from(requireContext()),
            binding.root,
            false
        )

        val sortDialogSheet = BottomSheetDialog(requireContext(), R.style.BottomSheetDialogTheme)

        sortDialogSheet.setContentView(sortBinding.root)

        sortBinding.btnSortAccepting.setOnClickListener {
            if(sortBinding.rbSortStandard.isChecked) {
                viewModel.setSort(SortCode.ByDefault)
            }
            if(sortBinding.rbSortByNameAscending.isChecked) {
                viewModel.setSort(SortCode.ByTitleAscending)
            }
            if(sortBinding.rbSortByNameDescending.isChecked) {
                viewModel.setSort(SortCode.ByTitleDescending)
            }
            if(sortBinding.rbSortBySalaryAscending.isChecked) {
                viewModel.setSort(SortCode.BySalaryAscending)
            }
            if(sortBinding.rbSortBySalaryDescending.isChecked) {
                viewModel.setSort(SortCode.BySalaryAscending)
            }
            if(sortBinding.rbSortByExperienceAscending.isChecked) {
                viewModel.setSort(SortCode.ByExperienceAscending)
            }
            if(sortBinding.rbSortByExperienceDescending.isChecked) {
                viewModel.setSort(SortCode.ByExperienceDescending)
            }
            sortDialogSheet.dismiss()
        }

        sortDialogSheet.show()
    }

    private fun setupObservers() {
        viewModel.jobList.observe(viewLifecycleOwner) {
            adapter.submitList(it)
        }
        viewModel.filterQuery.observe(viewLifecycleOwner) {
            viewModel.getJobsByFilter()
        }
    }
}