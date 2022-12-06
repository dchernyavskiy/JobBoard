package com.example.jobboard.ui.main.jobsearch.jobsearch.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.appcompat.content.res.AppCompatResources
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.jobboard.R
import com.example.jobboard.data.api.models.JobApiModel
import com.example.jobboard.databinding.RvJobListItemBinding
import com.example.jobboard.utils.ddMMyyyyFormatDate
import com.example.jobboard.utils.fullFormatDate

class JobAdapter : ListAdapter<JobApiModel, JobViewHolder>(JobDiffUtil()) {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): JobViewHolder {
        val binding = RvJobListItemBinding.inflate(
            LayoutInflater.from(parent.context),
            parent,
            false
        )
        return JobViewHolder(parent.context, binding)
    }

    override fun onBindViewHolder(holder: JobViewHolder, position: Int) {
        holder.bind(currentList[position])
    }
}

class JobViewHolder(
    private val context: Context,
    private val binding: RvJobListItemBinding
) : RecyclerView.ViewHolder(binding.root) {

    fun bind(job: JobApiModel) {
        val datePosted = fullFormatDate.parse(job.datePosted)
        val datePostedString = ddMMyyyyFormatDate.format(datePosted)
        binding.tvJobCity.text = job.location.city
        binding.tvJobVacation.text = job.name
        binding.tvJobPostedDate.text = datePostedString
        binding.tvJobPostedInfo.text = job.shortDescription
        binding.tvJobEmployment.text = job.employment
        binding.ivCompanyLogo.setImageDrawable(AppCompatResources.getDrawable(context, R.drawable.random_logo))
    }
}

class JobDiffUtil : DiffUtil.ItemCallback<JobApiModel>() {

    override fun areItemsTheSame(oldItem: JobApiModel, newItem: JobApiModel): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: JobApiModel, newItem: JobApiModel): Boolean {
        return oldItem == newItem
    }
}