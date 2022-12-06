package com.example.jobboard.domain.models.api

import com.google.gson.annotations.SerializedName

data class FilterQuery(
    @SerializedName("filters") val filters: Filters = Filters(),
    @SerializedName("pagging") val paging: Pagging = Pagging(),
    @SerializedName("sort") val sort: Sort = Sort()
)

data class Filters(
    @SerializedName("keyWord") val keyWord: String? = null,
    @SerializedName("categoryIds") val categoryIds: List<String>? = null,
    @SerializedName("locationIds") val locationIds: List<String>? = null,
    @SerializedName("salaryStart") val salaryStart: Int = 0,
    @SerializedName("salaryEnd") val salaryEnd: Int = 1000000,
    @SerializedName("emloyerIds") val employerIds: List<String>? = null,
    @SerializedName("experiences") val experiences: List<Int>? = null
)

data class Pagging(
    @SerializedName("page") val page: Int = 1,
    @SerializedName("count") val count: Int = 10

)

data class Sort(
    @SerializedName("sortByName") val sortByName: Boolean = false,
    @SerializedName("sortBySalary") val sortBySalary: Boolean = false,
    @SerializedName("sortByExpirience") val sortByExperience: Boolean = false,
    @SerializedName("isAscending") val isAscending: Boolean = false,
)