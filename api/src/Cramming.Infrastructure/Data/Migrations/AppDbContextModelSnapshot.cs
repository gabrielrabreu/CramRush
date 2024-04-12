﻿// <auto-generated />
using System;
using Cramming.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cramming.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.MultipleChoiceQuestionOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Statement")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("MultipleChoiceQuestionOption");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("TEXT");

                    b.Property<string>("Statement")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Question");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Question");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("Cramming.Domain.TopicAggregate.Question");

                    b.HasDiscriminator().HasValue("MultipleChoiceQuestion");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.OpenEndedQuestion", b =>
                {
                    b.HasBaseType("Cramming.Domain.TopicAggregate.Question");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("OpenEndedQuestion");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.MultipleChoiceQuestionOption", b =>
                {
                    b.HasOne("Cramming.Domain.TopicAggregate.MultipleChoiceQuestion", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Question", b =>
                {
                    b.HasOne("Cramming.Domain.TopicAggregate.Topic", "Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Tag", b =>
                {
                    b.HasOne("Cramming.Domain.TopicAggregate.Topic", "Topic")
                        .WithMany("Tags")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Cramming.Domain.TopicAggregate.Colour", "Colour", b1 =>
                        {
                            b1.Property<Guid>("TagId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("TagId");

                            b1.ToTable("Tags");

                            b1.WithOwner()
                                .HasForeignKey("TagId");
                        });

                    b.Navigation("Colour");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.Topic", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Cramming.Domain.TopicAggregate.MultipleChoiceQuestion", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
