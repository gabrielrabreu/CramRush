﻿// <auto-generated />
using System;
using Cramming.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cramming.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Topics", (string)null);
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionOptionData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Statement")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("TopicMultipleChoiceQuestionOptions", (string)null);
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicQuestionData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Statement")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicQuestions", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicTagData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicTags", (string)null);
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionData", b =>
                {
                    b.HasBaseType("Cramming.Infrastructure.Data.Entities.TopicQuestionData");

                    b.ToTable("TopicMultipleChoiceQuestions", (string)null);
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicOpenEndedQuestionData", b =>
                {
                    b.HasBaseType("Cramming.Infrastructure.Data.Entities.TopicQuestionData");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("TopicOpenEndedQuestions", (string)null);
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionOptionData", b =>
                {
                    b.HasOne("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionData", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicQuestionData", b =>
                {
                    b.HasOne("Cramming.Infrastructure.Data.Entities.TopicData", "Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicTagData", b =>
                {
                    b.HasOne("Cramming.Infrastructure.Data.Entities.TopicData", "Topic")
                        .WithMany("Tags")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionData", b =>
                {
                    b.HasOne("Cramming.Infrastructure.Data.Entities.TopicQuestionData", null)
                        .WithOne()
                        .HasForeignKey("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionData", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicOpenEndedQuestionData", b =>
                {
                    b.HasOne("Cramming.Infrastructure.Data.Entities.TopicQuestionData", null)
                        .WithOne()
                        .HasForeignKey("Cramming.Infrastructure.Data.Entities.TopicOpenEndedQuestionData", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicData", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Cramming.Infrastructure.Data.Entities.TopicMultipleChoiceQuestionData", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
