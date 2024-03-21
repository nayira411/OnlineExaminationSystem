using System;
using System.Collections.Generic;
using Examination.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination.Data;

public partial class ExamContext : DbContext
{
    public ExamContext()
    {
    }

    public ExamContext(DbContextOptions<ExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }
   
    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<InstructorCourse> InstructorCourses { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackCourse> TrackCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.;Database=ExaminationDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("PK__Admin__C69006288A1BE04D");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Aemail, "UQ__Admin__221408751DA40015").IsUnique();

            entity.HasIndex(e => e.Apassword, "UQ__Admin__93C2B8ED72C5F4B9").IsUnique();

            entity.Property(e => e.Aid).HasColumnName("AId");
            entity.Property(e => e.Aemail)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Aname)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apassword)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answers__D4825004B2E6E4F4");

            entity.Property(e => e.Answerbody)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Qid).HasColumnName("QId");

            entity.HasOne(d => d.QidNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Qid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answers_Questions");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CrId).HasName("PK__Courses__AD761DA43839E327");

            entity.HasIndex(e => e.Cname, "UQ__Courses__9F5E029982D921FF").IsUnique();

            entity.Property(e => e.Cname)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Passgrade).HasDefaultValue(60);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Eid).HasName("PK__Exams__C190176B4ADEEAEA");

            entity.Property(e => e.Eid).HasColumnName("EId");
            entity.Property(e => e.Tid).HasColumnName("TId");

            entity.HasOne(d => d.Cr).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exams_Courses");

            entity.HasOne(d => d.Ins).WithMany(p => p.Exams)
                .HasForeignKey(d => d.InsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exams_Instructor");

            entity.HasOne(d => d.TidNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Tid)
                .HasConstraintName("FK_Exams_Track");

            entity.HasMany(d => d.Qids).WithMany(p => p.Eids)
                .UsingEntity<Dictionary<string, object>>(
                    "ExamsQuestion",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("Qid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Exams_Questions_Questions"),
                    l => l.HasOne<Exam>().WithMany()
                        .HasForeignKey("Eid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Exams_Questions_Exams"),
                    j =>
                    {
                        j.HasKey("Eid", "Qid");
                        j.ToTable("Exams_Questions");
                        j.IndexerProperty<int>("Eid").HasColumnName("EId");
                        j.IndexerProperty<int>("Qid").HasColumnName("QId");
                    });
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InsId).HasName("PK__Instruct__9D104DEFD1D63C97");

            entity.ToTable("Instructor");

            entity.HasIndex(e => e.Inspassword, "UQ__Instruct__569305E9FC645637").IsUnique();

            entity.HasIndex(e => e.Insemail, "UQ__Instruct__C14D26C13637B689").IsUnique();

            entity.Property(e => e.Insemail)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Insgender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Insname)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Inspassword)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Inssalary).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<InstructorCourse>(entity =>
        {
            entity.HasKey(e => new { e.InsId, e.CrId });

            entity.ToTable("Instructor_Courses");

            entity.Property(e => e.Tid).HasColumnName("TId");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Qid).HasName("PK__Question__CAB1462B7330046F");

            entity.HasIndex(e => e.Qbody, "UQ__Question__ABA745AF30EBB3DE").IsUnique();

            entity.Property(e => e.Qid).HasColumnName("QId");
            entity.Property(e => e.Qbody)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Qtype)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Cr).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Courses");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PK__Students__CA195950F51D1EE6");

            entity.HasIndex(e => e.Password, "UQ__Students__6E2DBEDEC5EC9E2E").IsUnique();

            entity.HasIndex(e => e.Semail, "UQ__Students__9E5E9EA8C5FE6B07").IsUnique();

            entity.Property(e => e.Sid).HasColumnName("SId");
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Semail)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Sgender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Sname)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Track).WithMany(p => p.Students)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Track");

            entity.HasMany(d => d.Crs).WithMany(p => p.Sids)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CrId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Student_Courses_Courses"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("Sid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Student_Courses_Students"),
                    j =>
                    {
                        j.HasKey("Sid", "CrId");
                        j.ToTable("Student_Courses");
                        j.IndexerProperty<int>("Sid").HasColumnName("SId");
                    });
        });

        modelBuilder.Entity<StudentAnswer>(entity =>
        {
            entity.HasKey(e => new { e.Sid, e.Eid, e.Qid });

            entity.ToTable("Student_Answers");

            entity.Property(e => e.Sid).HasColumnName("SId");
            entity.Property(e => e.Eid).HasColumnName("EId");
            entity.Property(e => e.Qid).HasColumnName("QId");
            entity.Property(e => e.Sanswer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SAnswer");

            entity.HasOne(d => d.EidNavigation).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.Eid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answers_Exams");

            entity.HasOne(d => d.QidNavigation).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.Qid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answers_Questions");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.Sid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Answers_Students");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__022E0F5D71BE2F12");

            entity.Property(e => e.TopicDescription)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TopicName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("PK__Track__C456D74914119846");

            entity.ToTable("Track");

            entity.HasIndex(e => e.Tname, "UQ__Track__48581FED5097F7BA").IsUnique();

            entity.Property(e => e.Tid).HasColumnName("TId");
            entity.Property(e => e.Tname)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Track_Instructor");
        });

        modelBuilder.Entity<TrackCourse>(entity =>
        {
            entity.HasKey(e => new { e.Tid, e.Crid });

            entity.ToTable("Track_Courses");

            entity.Property(e => e.Tid).HasColumnName("TId");

            entity.HasOne(d => d.Cr).WithMany(p => p.TrackCourses)
                .HasForeignKey(d => d.Crid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Track_Courses_Courses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
