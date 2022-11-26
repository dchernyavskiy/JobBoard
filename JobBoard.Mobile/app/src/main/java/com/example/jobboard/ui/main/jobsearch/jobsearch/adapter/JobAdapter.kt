package com.example.jobboard.ui.main.jobsearch.jobsearch.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.jobboard.R
import com.example.jobboard.databinding.RvJobListItemBinding
import com.example.jobboard.domain.models.Job

class JobAdapter: ListAdapter<Job, JobViewHolder>(JobDiffUtil()) {

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

    fun bind(job: Job) {
        binding.tvJobCity.text = job.location.city
        binding.tvJobVacation.text = job.title
        binding.tvJobPostedDate.text = "2 month ago"
        binding.tvJobPostedInfo.text = job.shortDescription
        binding.ivCompanyLogo.setImageDrawable(context.getDrawable(R.drawable.random_logo))
    }
}

class JobDiffUtil : DiffUtil.ItemCallback<Job>() {

    override fun areItemsTheSame(oldItem: Job, newItem: Job): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: Job, newItem: Job): Boolean {
        return oldItem == newItem
    }
}