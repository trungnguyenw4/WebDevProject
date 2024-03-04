﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebDevelopmentProject.Models;

#nullable disable

namespace WebDevProject.Migrations
{
    [DbContext(typeof(InsuranceContext))]
    [Migration("20240301174440_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("WebDevProject.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CustomerId1")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.ToTable("C");
                });

            modelBuilder.Entity("WebDevProject.Models.FinancialInformation", b =>
                {
                    b.Property<int>("FinancialInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AnnualIncome")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Debts")
                        .HasColumnType("TEXT");

                    b.Property<int>("FinancialDependents")
                        .HasColumnType("INTEGER");

                    b.HasKey("FinancialInformationId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("FinancialInformation");
                });

            modelBuilder.Entity("WebDevProject.Models.HealthInformation", b =>
                {
                    b.Property<int>("HealthInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrentHealthStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LifestyleHabits")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MedicalHistory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("HealthInformationId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("HealthInformation");
                });

            modelBuilder.Entity("WebDevProject.Models.InsuranceClaim", b =>
                {
                    b.Property<int>("InsuranceClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ClaimAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ClaimDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimDetails")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("InsurancePolicyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InsuranceClaimId");

                    b.HasIndex("InsurancePolicyId");

                    b.ToTable("InsuranceClaim");
                });

            modelBuilder.Entity("WebDevProject.Models.InsurancePolicy", b =>
                {
                    b.Property<int>("InsurancePolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CoverageAmount")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PremiumAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("InsurancePolicyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("InsurancePolics");
                });

            modelBuilder.Entity("WebDevProject.Models.OccupationInformation", b =>
                {
                    b.Property<int>("OccupationInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmployerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmploymentStability")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("OccupationInformationId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("OccupationInformation");
                });

            modelBuilder.Entity("WebDevProject.Models.Customer", b =>
                {
                    b.HasOne("WebDevProject.Models.Customer", null)
                        .WithMany("Customers")
                        .HasForeignKey("CustomerId1");
                });

            modelBuilder.Entity("WebDevProject.Models.FinancialInformation", b =>
                {
                    b.HasOne("WebDevProject.Models.Customer", "Customer")
                        .WithOne("FinancialInformation")
                        .HasForeignKey("WebDevProject.Models.FinancialInformation", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebDevProject.Models.HealthInformation", b =>
                {
                    b.HasOne("WebDevProject.Models.Customer", "Customer")
                        .WithOne("HealthInformation")
                        .HasForeignKey("WebDevProject.Models.HealthInformation", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebDevProject.Models.InsuranceClaim", b =>
                {
                    b.HasOne("WebDevProject.Models.InsurancePolicy", "InsurancePolicy")
                        .WithMany("InsuranceClaims")
                        .HasForeignKey("InsurancePolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsurancePolicy");
                });

            modelBuilder.Entity("WebDevProject.Models.InsurancePolicy", b =>
                {
                    b.HasOne("WebDevProject.Models.Customer", "Customer")
                        .WithMany("InsurancePolicies")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebDevProject.Models.OccupationInformation", b =>
                {
                    b.HasOne("WebDevProject.Models.Customer", "Customer")
                        .WithOne("OccupationInformation")
                        .HasForeignKey("WebDevProject.Models.OccupationInformation", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebDevProject.Models.Customer", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("FinancialInformation")
                        .IsRequired();

                    b.Navigation("HealthInformation")
                        .IsRequired();

                    b.Navigation("InsurancePolicies");

                    b.Navigation("OccupationInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("WebDevProject.Models.InsurancePolicy", b =>
                {
                    b.Navigation("InsuranceClaims");
                });
#pragma warning restore 612, 618
        }
    }
}